using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using EnKolayCari2026.DataBridge.Catalog;
using EnKolayCari2026.DataBridge.Configuration;
using Microsoft.Data.SqlClient;

namespace EnKolayCari2026.DataBridge.Sync;

/// <summary>
/// Legacy MASTER/SLAVE → EKCN2026 şirket bazlı aktarım.
/// Id yeniden üretilir; FK'ler bellek + LegacyTransferMap ile çözülür; GId korunur (yoksa stable guid).
/// </summary>
public sealed class TransferOrchestrator
{
    private readonly AppConnections _connections;
    private readonly TransferOptions _options;
    private readonly TransferCatalogRoot _catalog;
    private readonly IProgress<TransferProgress>? _progress;
    private int _runInserted;
    private int _runSkipped;
    private int _runFailed;

    public TransferOrchestrator(
        AppConnections connections,
        TransferOptions options,
        TransferCatalogRoot catalog,
        IProgress<TransferProgress>? progress = null)
    {
        _connections = connections;
        _options = options;
        _catalog = catalog;
        _progress = progress;
    }

    public async Task<List<CompanyListItem>> LoadCompaniesAsync(CancellationToken ct = default)
    {
        var list = new List<CompanyListItem>();
        await using var conn = new SqlConnection(_connections.Master.BuildConnectionString());
        await conn.OpenAsync(ct);
        await using var cmd = new SqlCommand("""
            SELECT Id, GId, Kod, Unvan, ISNULL(Aktif, 1) AS Aktif
            FROM dbo.SirketTanim
            WHERE DeleteDateTime IS NULL
            ORDER BY Unvan
            """, conn);
        await using var reader = await cmd.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
        {
            list.Add(new CompanyListItem
            {
                LegacyId = Convert.ToInt64(reader.GetValue(0)),
                GId = reader.GetGuid(1),
                Code = reader.IsDBNull(2) ? "" : Convert.ToString(reader.GetValue(2)) ?? "",
                Title = reader.IsDBNull(3) ? "" : Convert.ToString(reader.GetValue(3)) ?? "",
                Stat = !reader.IsDBNull(4) && Convert.ToBoolean(reader.GetValue(4))
            });
        }
        return list;
    }

    public async Task<Dictionary<string, int>> PreviewCountsAsync(long legacyCompanyId, CancellationToken ct = default)
    {
        var result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        try
        {
            await using var conn = new SqlConnection(_connections.Master.BuildConnectionString());
            await conn.OpenAsync(ct);
            await using var cmd = new SqlCommand(
                "SELECT COUNT(1) FROM dbo.PersonelTanim WHERE SirketTanimId = @companyId", conn);
            cmd.Parameters.AddWithValue("@companyId", legacyCompanyId);
            result["PersonelTanim"] = Convert.ToInt32(await cmd.ExecuteScalarAsync(ct));
        }
        catch (Exception ex)
        {
            result["PersonelTanim"] = -1;
            Report("PersonelTanim", 0, 0, $"Preview hata: {ex.Message}");
        }

        foreach (var table in _catalog.Tables.OrderBy(t => t.Order))
        {
            ct.ThrowIfCancellationRequested();
            try
            {
                result[table.LegacyTable] = await CountSourceAsync(table, legacyCompanyId, ct);
            }
            catch (Exception ex)
            {
                result[table.LegacyTable] = -1;
                Report(table.LegacyTable, 0, 0, $"Preview hata: {ex.Message}");
            }
        }
        return result;
    }

    public async Task TestConnectionsAsync(CancellationToken ct = default)
    {
        foreach (var (name, ep) in new[]
                 {
                     ("MASTER", _connections.Master),
                     ("SLAVE", _connections.Slave),
                     ("TARGET", _connections.Target)
                 })
        {
            await using var conn = new SqlConnection(ep.BuildConnectionString());
            await conn.OpenAsync(ct);
            await using var cmd = new SqlCommand("SELECT 1", conn);
            await cmd.ExecuteScalarAsync(ct);
            Report(name, 1, 1, $"{name} bağlantı OK ({ep.Database})");
        }
    }

    public async Task<TransferRunResult> RunAsync(CompanyListItem company, CancellationToken ct = default)
    {
        var run = new TransferRunResult
        {
            StartedAt = DateTime.Now,
            LegacyCompanyId = company.LegacyId
        };

        _runInserted = 0;
        _runSkipped = 0;
        _runFailed = 0;
        Report("RUN", 0, 0, "Aktarım başladı…");

        var idMap = new Dictionary<string, Dictionary<long, long>>(StringComparer.OrdinalIgnoreCase);

        try
        {
            await using var target = new SqlConnection(_connections.Target.BuildConnectionString());
            await target.OpenAsync(ct);

            var companyDef = _catalog.Tables.First(t => t.LegacyTable == "SirketTanim");
            var companyResult = await TransferTableAsync(
                companyDef, company.LegacyId, targetCompanyId: null, idMap, onlyLegacyId: company.LegacyId, target, ct);
            run.Tables.Add(companyResult);

            if (!idMap.TryGetValue("SirketTanim", out var companyIds) ||
                !companyIds.TryGetValue(company.LegacyId, out var newCompanyId))
            {
                newCompanyId = await ResolveExistingCompanyIdAsync(target, company.GId, ct);
                if (newCompanyId <= 0)
                    throw new InvalidOperationException("Hedef Company oluşturulamadı / bulunamadı.");
                idMap["SirketTanim"] = new Dictionary<long, long> { [company.LegacyId] = newCompanyId };
            }

            run.NewCompanyId = newCompanyId;
            Report("Company", 1, 1, $"Hedef CompanyId={newCompanyId} (legacy {company.LegacyId})");

            // Personal AI mimari tablosu katalogda yok; Token/DepoPersonel vb. FK için önce aktar
            var personalResult = await TransferPersonelTanimAsync(
                company.LegacyId, newCompanyId, company.Code, idMap, target, ct);
            run.Tables.Add(personalResult);

            foreach (var table in _catalog.Tables.Where(t => t.LegacyTable != "SirketTanim").OrderBy(t => t.Order))
            {
                ct.ThrowIfCancellationRequested();
                var tr = await TransferTableAsync(table, company.LegacyId, newCompanyId, idMap, onlyLegacyId: null, target, ct);
                run.Tables.Add(tr);
            }
        }
        catch (OperationCanceledException)
        {
            run.Cancelled = true;
            run.Errors.Add("Aktarım iptal edildi.");
        }
        catch (Exception ex)
        {
            run.Errors.Add(ex.Message);
            Report("RUN", 0, 0, "HATA: " + ex.Message);
        }

        run.FinishedAt = DateTime.Now;
        return run;
    }

    private async Task<long> ResolveExistingCompanyIdAsync(SqlConnection target, Guid gId, CancellationToken ct)
    {
        await using var cmd = new SqlCommand("SELECT Id FROM common.Company WHERE GId = @g", target);
        cmd.Parameters.AddWithValue("@g", gId);
        var o = await cmd.ExecuteScalarAsync(ct);
        return o == null || o == DBNull.Value ? 0 : Convert.ToInt64(o);
    }

    // ─────────────────────────────────────────────────────────────────────
    //  Sil-Baştan: hedef DB'den şirkete ait tüm verileri temizle
    // ─────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Hedef veritabanından seçili şirketin tüm verilerini siler (reverse FK sırası).
    /// LegacyTransferMap, common.Personal ve common.Company dahil.
    /// </summary>
    public async Task<CleanResult> CleanCompanyAsync(CompanyListItem company, CancellationToken ct = default)
    {
        var result = new CleanResult { LegacyCompanyId = company.LegacyId };

        await using var target = new SqlConnection(_connections.Target.BuildConnectionString());
        await target.OpenAsync(ct);

        // Hedef Company Id'yi bul
        var targetCompanyId = await ResolveExistingCompanyIdAsync(target, company.GId, ct);
        if (targetCompanyId <= 0)
        {
            result.Warnings.Add($"Hedef DB'de bu şirket bulunamadı (GId={company.GId}). Zaten temiz.");
            Report("CLEAN", 0, 0, "Hedef DB'de şirket kaydı yok — temizlenecek veri yok.");
            return result;
        }

        result.TargetCompanyId = targetCompanyId;
        Report("CLEAN", 0, 0, $"Temizleme başlıyor — TargetCompanyId={targetCompanyId}");

        // Tabloları yüksek order'dan düşüğe doğru sil (FK ters)
        var tablesInReverseOrder = _catalog.Tables
            .Where(t => t.CompanyFilter && t.LegacyTable != "SirketTanim")
            .OrderByDescending(t => t.Order)
            .ThenBy(t => t.QualifiedNewTable);

        foreach (var tbl in tablesInReverseOrder)
        {
            ct.ThrowIfCancellationRequested();
            try
            {
                var deleted = await DeleteByCompanyIdAsync(target, tbl.QualifiedNewTable, targetCompanyId, ct);
                result.DeletedRows[tbl.QualifiedNewTable] = deleted;
                Report("CLEAN", 0, 0, $"  {tbl.QualifiedNewTable}: {deleted} satır silindi.");
            }
            catch (Exception ex)
            {
                var msg = $"FAIL silerken {tbl.QualifiedNewTable}: {ex.Message}";
                result.Errors.Add(msg);
                Report("CLEAN", 0, 0, msg);
            }
        }

        // common.Personal (PersonelTanim'dan aktarılan)
        try
        {
            var deleted = await DeleteByCompanyIdAsync(target, "common.Personal", targetCompanyId, ct);
            result.DeletedRows["common.Personal"] = deleted;
            Report("CLEAN", 0, 0, $"  common.Personal: {deleted} satır silindi.");
        }
        catch (Exception ex)
        {
            result.Warnings.Add($"common.Personal silinemedi (tablo yoksa normal): {ex.Message}");
        }

        // LegacyTransferMap — şirkete ait satırları temizle
        try
        {
            await using var mapCmd = new SqlCommand(
                "DELETE FROM common.LegacyTransferMap WHERE CompanyId = @cid", target);
            mapCmd.Parameters.AddWithValue("@cid", targetCompanyId);
            var deleted = await mapCmd.ExecuteNonQueryAsync(ct);
            result.DeletedRows["common.LegacyTransferMap"] = deleted;
            Report("CLEAN", 0, 0, $"  common.LegacyTransferMap: {deleted} satır silindi.");
        }
        catch (Exception ex)
        {
            result.Warnings.Add($"LegacyTransferMap temizlenemedi: {ex.Message}");
        }

        // Son olarak Company kaydını sil
        try
        {
            await using var delCompany = new SqlCommand(
                "DELETE FROM common.Company WHERE Id = @cid", target);
            delCompany.Parameters.AddWithValue("@cid", targetCompanyId);
            await delCompany.ExecuteNonQueryAsync(ct);
            result.DeletedRows["common.Company"] = 1;
            Report("CLEAN", 0, 0, $"  common.Company: silindi (Id={targetCompanyId}).");
        }
        catch (Exception ex)
        {
            result.Errors.Add($"common.Company silinemedi: {ex.Message}");
            Report("CLEAN", 0, 0, $"FAIL common.Company: {ex.Message}");
        }

        var totalDeleted = result.DeletedRows.Values.Sum();
        Report("CLEAN", 1, 1, $"Temizleme tamamlandı — toplam {totalDeleted:N0} satır silindi.");
        return result;
    }

    private static async Task<int> DeleteByCompanyIdAsync(SqlConnection target, string qualifiedTable, long companyId, CancellationToken ct)
    {
        await using var cmd = new SqlCommand($"DELETE FROM {qualifiedTable} WHERE CompanyId = @cid", target);
        cmd.Parameters.AddWithValue("@cid", companyId);
        return await cmd.ExecuteNonQueryAsync(ct);
    }

    private async Task<int> CountSourceAsync(TransferTableDef table, long legacyCompanyId, CancellationToken ct)
    {
        await using var conn = new SqlConnection(GetSourceCs(table));
        await conn.OpenAsync(ct);

        var sql = new StringBuilder($"SELECT COUNT(1) FROM dbo.[{table.LegacyTable}] WHERE 1=1");
        if (table.LegacyTable == "SirketTanim")
            sql.Append(" AND Id = @companyId");
        else if (table.CompanyFilter)
            sql.Append(" AND SirketTanimId = @companyId");

        await using var cmd = new SqlCommand(sql.ToString(), conn);
        if (table.LegacyTable == "SirketTanim" || table.CompanyFilter)
            cmd.Parameters.AddWithValue("@companyId", legacyCompanyId);

        return Convert.ToInt32(await cmd.ExecuteScalarAsync(ct));
    }

    private async Task<TableTransferResult> TransferTableAsync(
        TransferTableDef table,
        long legacyCompanyId,
        long? targetCompanyId,
        Dictionary<string, Dictionary<long, long>> idMap,
        long? onlyLegacyId,
        SqlConnection target,
        CancellationToken ct)
    {
        var result = new TableTransferResult
        {
            LegacyTable = table.LegacyTable,
            NewTable = table.QualifiedNewTable
        };

        Report(table.LegacyTable, 0, 0, $"{table.LegacyTable} → {table.QualifiedNewTable} başlıyor…");

        HashSet<string> sourceCols;
        HashSet<string> targetCols;
        try
        {
            sourceCols = await GetColumnsAsync(GetSourceCs(table), "dbo", table.LegacyTable, ct);
            targetCols = await GetColumnsAsync(_connections.Target.BuildConnectionString(), table.NewSchema, table.NewTable, ct);
        }
        catch (Exception ex)
        {
            result.Failed++;
            _runFailed++;
            var note = FailDiagnostics.Format(table.LegacyTable, table.QualifiedNewTable, null, ex);
            result.Errors.Add(note);
            Report(table.LegacyTable, 0, 0, note);
            return result;
        }

        var hasTargetId = targetCols.Contains("Id");
        var hasTargetGId = targetCols.Contains("GId");
        var hasSourceId = sourceCols.Contains("Id");

        // Kaynak kolon adlarını çözümle (KayıtYeri vs KayitYeri vb.)
        var resolved = new List<(TransferColumnDef Col, string? SourceName)>();
        foreach (var c in table.Columns.Where(c => c.Role != "legacyId"))
        {
            if (!targetCols.Contains(c.New))
                continue;
            if (c.Role is "company" or "gid")
            {
                resolved.Add((c, ResolveSourceColumn(sourceCols, c.Legacy)));
                continue;
            }
            var src = ResolveSourceColumn(sourceCols, c.Legacy);
            if (src != null)
                resolved.Add((c, src));
        }

        var usable = resolved.Select(r => r.Col).ToList();
        var legacyAlias = resolved
            .Where(r => r.SourceName != null)
            .ToDictionary(r => r.Col.New, r => r.SourceName!, StringComparer.OrdinalIgnoreCase);

        // CentralCurrency gibi Id'siz tablolar: katalog kolonları yeterli
        if (usable.Count == 0)
        {
            var note =
                $"FAIL {table.LegacyTable} → {table.QualifiedNewTable} | eşleşen kolon yok | ÇÖZÜM: kaynak dbo.{table.LegacyTable} ile hedef {table.QualifiedNewTable} kolon adlarını ve Catalog/transfer-catalog.json map'ini karşılaştır; gen_transfer_catalog.py çalıştır.";
            result.Errors.Add(note);
            result.Failed++;
            _runFailed++;
            Report(table.LegacyTable, 0, 0, note);
            return result;
        }

        var selectCols = usable
            .Where(c => c.Role != "company" || legacyAlias.ContainsKey(c.New))
            .Where(c => c.Role != "gid" || legacyAlias.ContainsKey(c.New))
            .Select(c => legacyAlias.TryGetValue(c.New, out var sn) ? sn : c.Legacy)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (hasSourceId && !selectCols.Contains("Id", StringComparer.OrdinalIgnoreCase))
            selectCols.Insert(0, "Id");
        foreach (var a in new[] { "InsertDateTime", "UpdateDateTime", "DeleteDateTime", "GId", "Kod", "Code" })
        {
            var resolvedA = ResolveSourceColumn(sourceCols, a);
            if (resolvedA != null && !selectCols.Contains(resolvedA, StringComparer.OrdinalIgnoreCase))
                selectCols.Add(resolvedA);
        }

        var sql = new StringBuilder();
        sql.Append("SELECT ").Append(string.Join(", ", selectCols.Select(c => $"[{c}]")));
        sql.Append($" FROM dbo.[{table.LegacyTable}] WHERE 1=1");
        if (onlyLegacyId.HasValue)
            sql.Append(" AND Id = @onlyId");
        else if (table.CompanyFilter)
            sql.Append(" AND SirketTanimId = @companyId");

        await using var source = new SqlConnection(GetSourceCs(table));
        await source.OpenAsync(ct);
        await using var cmd = new SqlCommand(sql.ToString(), source);
        if (onlyLegacyId.HasValue)
            cmd.Parameters.AddWithValue("@onlyId", onlyLegacyId.Value);
        else if (table.CompanyFilter)
            cmd.Parameters.AddWithValue("@companyId", legacyCompanyId);

        var rows = new List<Dictionary<string, object?>>();
        await using (var reader = await cmd.ExecuteReaderAsync(ct))
        {
            while (await reader.ReadAsync(ct))
            {
                var row = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
                for (var i = 0; i < reader.FieldCount; i++)
                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                rows.Add(row);
            }
        }

        result.SourceCount = rows.Count;
        if (!idMap.ContainsKey(table.LegacyTable))
            idMap[table.LegacyTable] = new Dictionary<long, long>();

        var mapCompanyId = targetCompanyId ?? ResolveCompanyId(idMap, legacyCompanyId);
        var iRow = 0;
        foreach (var row in rows)
        {
            ct.ThrowIfCancellationRequested();
            iRow++;
            try
            {
                long legacyId = hasSourceId && row.TryGetValue("Id", out var idObj) && idObj != null
                    ? Convert.ToInt64(idObj)
                    : StableLongKey(table.LegacyTable, row);

                var gId = ResolveRowGId(table, row, legacyId, hasSourceGId: sourceCols.Contains("GId"));

                // Skip: GId varsa hedefte; yoksa LegacyTransferMap
                if (_options.SkipExistingByGId)
                {
                    long existingId = 0;
                    if (hasTargetGId)
                        existingId = await FindTargetIdByGIdAsync(target, table, gId, ct);
                    else
                        existingId = await FindMappedNewIdAsync(target, table.LegacyTable, gId, ct);

                    // Code-based tables (CentralCurrency)
                    if (existingId == 0 && !hasTargetId && targetCols.Contains("Code") &&
                        row.TryGetValue("Kod", out var kodObj) && kodObj != null)
                    {
                        existingId = await FindCodeExistsAsync(target, table, Convert.ToString(kodObj)!, ct)
                            ? -1
                            : 0;
                    }

                    if (existingId != 0)
                    {
                        if (existingId > 0)
                            idMap[table.LegacyTable][legacyId] = existingId;
                        result.Skipped++;
                        _runSkipped++;
                        ProgressEvery(table, iRow, rows.Count, result);
                        continue;
                    }
                }

                var insertCols = new List<string>();
                var insertVals = new List<object?>();

                foreach (var col in usable)
                {
                    var legacyCol = legacyAlias.TryGetValue(col.New, out var alias) ? alias : col.Legacy;
                    object? value = col.Role switch
                    {
                        "company" => mapCompanyId,
                        "gid" => gId,
                        "fk" => RemapFk(row, col, idMap, legacyCol),
                        _ => row.TryGetValue(legacyCol, out var v) ? v : null
                    };

                    if (col.Role == "fk" && value == null && IsLikelyRequired(col.New))
                        value = 0L;

                    value = ApplyColumnDefault(col.New, value);

                    insertCols.Add(col.New);
                    insertVals.Add(Normalize(value));
                }

                EnsureRequiredDefaults(table, targetCols, insertCols, insertVals);

                long newId;
                if (hasTargetId)
                    newId = await InsertRowAsync(target, table, insertCols, insertVals, ct);
                else
                {
                    await InsertRowNoIdAsync(target, table, insertCols, insertVals, ct);
                    newId = legacyId; // sentetik
                }

                idMap[table.LegacyTable][legacyId] = newId;
                await UpsertTransferMapAsync(target, mapCompanyId, table, gId, newId, hasTargetGId, hasTargetId, row, ct);
                result.Inserted++;
                _runInserted++;
            }
            catch (Exception ex)
            {
                result.Failed++;
                _runFailed++;
                var note = FailDiagnostics.Format(
                    table.LegacyTable,
                    table.QualifiedNewTable,
                    row.GetValueOrDefault("Id"),
                    ex,
                    row);
                if (result.Errors.Count < 200)
                    result.Errors.Add(note);
                // Her fail anında raporla (UI'da "Sadece FAIL" filtresi ile izlenebilir)
                Report(table.LegacyTable, iRow, rows.Count, note);
            }

            ProgressEvery(table, iRow, rows.Count, result);
        }

        Report(table.LegacyTable, rows.Count, rows.Count,
            $"{table.LegacyTable} bitti: ins={result.Inserted}, skip={result.Skipped}, fail={result.Failed}");
        return result;
    }

    private void ProgressEvery(TransferTableDef table, int iRow, int total, TableTransferResult result)
    {
        if (iRow % 10 == 0 || iRow == total)
            Report(table.LegacyTable, iRow, total,
                $"{table.LegacyTable}: {iRow}/{total}");
    }

    private static long ResolveCompanyId(Dictionary<string, Dictionary<long, long>> idMap, long legacyCompanyId)
    {
        if (idMap.TryGetValue("SirketTanim", out var m) && m.TryGetValue(legacyCompanyId, out var id))
            return id;
        return 0;
    }

    private async Task<long> FindMappedNewIdAsync(SqlConnection target, string legacyTable, Guid legacyGId, CancellationToken ct)
    {
        await using var cmd = new SqlCommand("""
            SELECT CASE WHEN EXISTS (
                SELECT 1 FROM common.LegacyTransferMap WHERE LegacyTableName = @lt AND LegacyGId = @lg
            ) THEN CAST(1 AS bigint) ELSE CAST(0 AS bigint) END
            """, target);
        cmd.Parameters.AddWithValue("@lt", legacyTable);
        cmd.Parameters.AddWithValue("@lg", legacyGId);
        var o = await cmd.ExecuteScalarAsync(ct);
        return o != null && Convert.ToInt64(o) == 1 ? -1 : 0;
    }

    private async Task<bool> FindCodeExistsAsync(SqlConnection target, TransferTableDef table, string code, CancellationToken ct)
    {
        await using var cmd = new SqlCommand($"SELECT CASE WHEN EXISTS (SELECT 1 FROM {table.QualifiedNewTable} WHERE Code = @c) THEN 1 ELSE 0 END", target);
        cmd.Parameters.AddWithValue("@c", code);
        return Convert.ToInt32(await cmd.ExecuteScalarAsync(ct)) == 1;
    }

    private async Task UpsertTransferMapAsync(
        SqlConnection target,
        long targetCompanyId,
        TransferTableDef table,
        Guid legacyGId,
        long newId,
        bool hasTargetGId,
        bool hasTargetId,
        Dictionary<string, object?> row,
        CancellationToken ct)
    {
        Guid newGId = legacyGId;
        if (hasTargetGId && hasTargetId)
        {
            await using var get = new SqlCommand($"SELECT GId FROM {table.QualifiedNewTable} WHERE Id = @id", target);
            get.Parameters.AddWithValue("@id", newId);
            var o = await get.ExecuteScalarAsync(ct);
            if (o is Guid g) newGId = g;
        }

        DateTime? lastChange = null;
        foreach (var key in new[] { "InsertDateTime", "UpdateDateTime", "DeleteDateTime" })
        {
            if (row.TryGetValue(key, out var v) && v is DateTime dt)
            {
                if (lastChange == null || dt > lastChange)
                    lastChange = dt;
            }
        }

        await using var cmd = new SqlCommand("""
            IF EXISTS (SELECT 1 FROM common.LegacyTransferMap WHERE LegacyTableName = @lt AND LegacyGId = @lg)
                UPDATE common.LegacyTransferMap
                SET NewGId = @ng, CompanyId = @cid, LegacyLastChangeDate = @lcd, NewTableName = @nt
                WHERE LegacyTableName = @lt AND LegacyGId = @lg;
            ELSE
                INSERT INTO common.LegacyTransferMap
                    (GId, CompanyId, LegacyTableName, LegacyGId, LegacyLastChangeDate, NewTableName, NewGId, InsertDateTime)
                VALUES
                    (NEWID(), @cid, @lt, @lg, @lcd, @nt, @ng, SYSUTCDATETIME());
            """, target);
        cmd.Parameters.AddWithValue("@lt", table.LegacyTable);
        cmd.Parameters.AddWithValue("@lg", legacyGId);
        cmd.Parameters.AddWithValue("@ng", newGId);
        cmd.Parameters.AddWithValue("@cid", targetCompanyId);
        cmd.Parameters.AddWithValue("@lcd", (object?)lastChange ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@nt", $"{table.NewSchema}.{table.NewTable}");
        await cmd.ExecuteNonQueryAsync(ct);
    }

    private string GetSourceCs(TransferTableDef table) =>
        table.SourceDb.Equals("MASTER", StringComparison.OrdinalIgnoreCase)
            ? _connections.Master.BuildConnectionString()
            : _connections.Slave.BuildConnectionString();

    private static async Task<HashSet<string>> GetColumnsAsync(string cs, string schema, string table, CancellationToken ct)
    {
        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        await using var conn = new SqlConnection(cs);
        await conn.OpenAsync(ct);
        await using var cmd = new SqlCommand("""
            SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = @s AND TABLE_NAME = @t
            """, conn);
        cmd.Parameters.AddWithValue("@s", schema);
        cmd.Parameters.AddWithValue("@t", table);
        await using var reader = await cmd.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
            set.Add(reader.GetString(0));
        return set;
    }

    private static bool IsLikelyRequired(string col) =>
        col is "AccountId" or "BrandId" or "UnitId"
            or "CashRegisterId" or "PersonalId";

    private static object? ApplyColumnDefault(string col, object? value)
    {
        if (col == "RecordSource" && (value == null || value == DBNull.Value))
            return 0;
        // BinData, ReportData vb. binary alanlar null gelirse boş byte[] yaz (NOT NULL kısıtı)
        if ((col == "BinData" || col == "ReportData" || col == "FileData" || col == "Content") 
            && (value == null || value == DBNull.Value))
            return Array.Empty<byte>();
        return value;
    }

    private static void EnsureRequiredDefaults(TransferTableDef table, HashSet<string> targetCols, List<string> insertCols, List<object?> insertVals)
    {
        // InsertDateTime zorunluysa ve yoksa ekle
        if (targetCols.Contains("InsertDateTime") && !insertCols.Contains("InsertDateTime", StringComparer.OrdinalIgnoreCase))
        {
            insertCols.Add("InsertDateTime");
            insertVals.Add(DateTime.UtcNow);
        }
    }

    private static object? RemapFk(
        Dictionary<string, object?> row,
        TransferColumnDef col,
        Dictionary<string, Dictionary<long, long>> idMap,
        string legacyCol)
    {
        if (!row.TryGetValue(legacyCol, out var raw) || raw == null || raw == DBNull.Value)
            return null;

        long legacyFkId;
        try { legacyFkId = Convert.ToInt64(raw); }
        catch { return null; }

        if (legacyFkId <= 0)
            return null;

        var fkTable = col.FkTable;
        if (string.IsNullOrEmpty(fkTable))
            return null;

        if (idMap.TryGetValue(fkTable, out var map) && map.TryGetValue(legacyFkId, out var newId))
            return newId;

        return null;
    }

    private static readonly DateTime SqlDateTimeMin = new(1753, 1, 1);
    private static readonly DateTime SqlDateTimeMax = new(9999, 12, 31);

    private static object? Normalize(object? value)
    {
        if (value == null || value == DBNull.Value)
            return null;
        if (value is DateTime dt)
        {
            if (dt < SqlDateTimeMin || dt > SqlDateTimeMax)
                return null;
        }
        return value;
    }

    private static Guid ResolveRowGId(TransferTableDef table, Dictionary<string, object?> row, long legacyId, bool hasSourceGId)
    {
        if (hasSourceGId && row.TryGetValue("GId", out var gObj) && gObj is Guid g && g != Guid.Empty)
            return g;
        // Stable guid from legacyTable + legacyId
        return StableGuid(table.LegacyTable, legacyId);
    }

    private static Guid StableGuid(string tableName, long legacyId)
    {
        var input = $"{tableName}:{legacyId}";
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        // Take first 16 bytes and make a valid guid
        var guidBytes = bytes[..16];
        guidBytes[6] = (byte)((guidBytes[6] & 0x0F) | 0x50); // version 5
        guidBytes[8] = (byte)((guidBytes[8] & 0x3F) | 0x80); // variant
        return new Guid(guidBytes);
    }

    private static long StableLongKey(string tableName, Dictionary<string, object?> row)
    {
        // Use Code/Kod as a stable key for tables without Id
        if (row.TryGetValue("Kod", out var kod) && kod != null)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes($"{tableName}:{kod}"));
            return Math.Abs(BitConverter.ToInt64(bytes, 0));
        }
        if (row.TryGetValue("Code", out var code) && code != null)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes($"{tableName}:{code}"));
            return Math.Abs(BitConverter.ToInt64(bytes, 0));
        }
        return Random.Shared.NextInt64(1, long.MaxValue);
    }

    private static async Task<long> FindTargetIdByGIdAsync(SqlConnection target, TransferTableDef table, Guid gId, CancellationToken ct)
    {
        await using var cmd = new SqlCommand($"SELECT ISNULL(Id, 0) FROM {table.QualifiedNewTable} WHERE GId = @g", target);
        cmd.Parameters.AddWithValue("@g", gId);
        var o = await cmd.ExecuteScalarAsync(ct);
        return o == null || o == DBNull.Value ? 0 : Convert.ToInt64(o);
    }

    private static async Task<long> InsertRowAsync(
        SqlConnection target,
        TransferTableDef table,
        List<string> cols,
        List<object?> vals,
        CancellationToken ct)
    {
        var nonIdCols = cols.Where(c => !c.Equals("Id", StringComparison.OrdinalIgnoreCase)).ToList();
        var nonIdVals = vals.Where((_, i) => !cols[i].Equals("Id", StringComparison.OrdinalIgnoreCase)).ToList();

        var colList = string.Join(", ", nonIdCols.Select(c => $"[{c}]"));
        var paramList = string.Join(", ", nonIdCols.Select((_, i) => $"@p{i}"));
        var sql = $"INSERT INTO {table.QualifiedNewTable} ({colList}) OUTPUT INSERTED.Id VALUES ({paramList})";

        await using var cmd = new SqlCommand(sql, target) { CommandTimeout = 300 };
        for (var i = 0; i < nonIdCols.Count; i++)
            cmd.Parameters.AddWithValue($"@p{i}", nonIdVals[i] ?? DBNull.Value);

        var result = await cmd.ExecuteScalarAsync(ct);
        return result == null || result == DBNull.Value ? 0 : Convert.ToInt64(result);
    }

    private static async Task InsertRowNoIdAsync(
        SqlConnection target,
        TransferTableDef table,
        List<string> cols,
        List<object?> vals,
        CancellationToken ct)
    {
        var colList = string.Join(", ", cols.Select(c => $"[{c}]"));
        var paramList = string.Join(", ", cols.Select((_, i) => $"@p{i}"));
        var sql = $"INSERT INTO {table.QualifiedNewTable} ({colList}) VALUES ({paramList})";

        await using var cmd = new SqlCommand(sql, target) { CommandTimeout = 300 };
        for (var i = 0; i < cols.Count; i++)
            cmd.Parameters.AddWithValue($"@p{i}", vals[i] ?? DBNull.Value);

        await cmd.ExecuteNonQueryAsync(ct);
    }

    /// <summary>
    /// Kaynak kolon adını çözümler: önce tam eşleşme, sonra Türkçe harf katlama ile.
    /// Ör: "KayitYeri" → "KayıtYeri" varsa onu döner.
    /// </summary>
    private static string? ResolveSourceColumn(HashSet<string> sourceCols, string legacyName)
    {
        if (sourceCols.Contains(legacyName))
            return legacyName;
        // Türkçe harf katlama: ı→i, İ→I, ğ→g, Ğ→G, ş→s, Ş→S, ü→u, Ü→U, ö→o, Ö→O, ç→c, Ç→C
        var folded = FoldTr(legacyName);
        var match = sourceCols.FirstOrDefault(c => FoldTr(c) == folded);
        return match;
    }

    private static string FoldTr(string s) =>
        s.Replace("ı", "i", StringComparison.Ordinal)
         .Replace("İ", "I", StringComparison.Ordinal)
         .Replace("ğ", "g", StringComparison.Ordinal)
         .Replace("Ğ", "G", StringComparison.Ordinal)
         .Replace("ş", "s", StringComparison.Ordinal)
         .Replace("Ş", "S", StringComparison.Ordinal)
         .Replace("ü", "u", StringComparison.Ordinal)
         .Replace("Ü", "U", StringComparison.Ordinal)
         .Replace("ö", "o", StringComparison.Ordinal)
         .Replace("Ö", "O", StringComparison.Ordinal)
         .Replace("ç", "c", StringComparison.Ordinal)
         .Replace("Ç", "C", StringComparison.Ordinal);

    private async Task<TableTransferResult> TransferPersonelTanimAsync(
        long legacyCompanyId,
        long targetCompanyId,
        string companyCode,
        Dictionary<string, Dictionary<long, long>> idMap,
        SqlConnection target,
        CancellationToken ct)
    {
        var result = new TableTransferResult
        {
            LegacyTable = "PersonelTanim",
            NewTable = "common.Personal"
        };
        Report("PersonelTanim", 0, 0, "PersonelTanim → common.Personal başlıyor…");

        try
        {
            var sourceCols = await GetColumnsAsync(_connections.Master.BuildConnectionString(), "dbo", "PersonelTanim", ct);

            await using var src = new SqlConnection(_connections.Master.BuildConnectionString());
            await src.OpenAsync(ct);
            // SELECT * — gerçek kolon adlarını okuyoruz; sorguda hardcode yok
            await using var cmd = new SqlCommand(
                "SELECT * FROM dbo.PersonelTanim WHERE SirketTanimId = @cid",
                src);
            cmd.Parameters.AddWithValue("@cid", legacyCompanyId);

            var rows = new List<Dictionary<string, object?>>();
            await using (var reader = await cmd.ExecuteReaderAsync(ct))
            {
                while (await reader.ReadAsync(ct))
                {
                    var row = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
                    for (var i = 0; i < reader.FieldCount; i++)
                        row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    rows.Add(row);
                }
            }
            result.SourceCount = rows.Count;

            if (!idMap.ContainsKey("PersonelTanim"))
                idMap["PersonelTanim"] = new Dictionary<long, long>();

            var targetCols = await GetColumnsAsync(_connections.Target.BuildConnectionString(), "common", "Personal", ct);
            var hasTargetGId = targetCols.Contains("GId");

            var iRow = 0;
            foreach (var row in rows)
            {
                ct.ThrowIfCancellationRequested();
                iRow++;
                try
                {
                    var legacyId = row.TryGetValue("Id", out var idObj) && idObj != null
                        ? Convert.ToInt64(idObj)
                        : 0L;
                    var gId = row.TryGetValue("GId", out var gObj) && gObj is Guid g && g != Guid.Empty
                        ? g
                        : StableGuid("PersonelTanim", legacyId);

                    // Skip check
                    if (_options.SkipExistingByGId && hasTargetGId)
                    {
                        await using var chk = new SqlCommand("SELECT ISNULL(Id,0) FROM common.Personal WHERE GId = @g", target);
                        chk.Parameters.AddWithValue("@g", gId);
                        var existObj = await chk.ExecuteScalarAsync(ct);
                        var existId = existObj == null || existObj == DBNull.Value ? 0 : Convert.ToInt64(existObj);
                        if (existId > 0)
                        {
                            idMap["PersonelTanim"][legacyId] = existId;
                            result.Skipped++;
                            _runSkipped++;
                            continue;
                        }
                    }

                    // Build columns to insert
                    var insertCols = new List<string>();
                    var insertVals = new List<object?>();

                    void AddCol(string col, object? val)
                    {
                        if (targetCols.Contains(col))
                        {
                            insertCols.Add(col);
                            insertVals.Add(Normalize(val));
                        }
                    }

                    if (hasTargetGId) AddCol("GId", gId);
                    AddCol("CompanyId", targetCompanyId);

                    // Kolon adlarını dinamik çöz (Kod/PersonelKodu/Code vb. fark etmez)
                    var colCode     = ResolveSourceColumn(sourceCols, "Kod")
                                   ?? ResolveSourceColumn(sourceCols, "PersonelKodu")
                                   ?? ResolveSourceColumn(sourceCols, "Code");
                    var colFirst    = ResolveSourceColumn(sourceCols, "Ad")
                                   ?? ResolveSourceColumn(sourceCols, "PersonelAdi")
                                   ?? ResolveSourceColumn(sourceCols, "FirstName");
                    var colLast     = ResolveSourceColumn(sourceCols, "Soyad")
                                   ?? ResolveSourceColumn(sourceCols, "PersonelSoyadi")
                                   ?? ResolveSourceColumn(sourceCols, "LastName");
                    var colEmail    = ResolveSourceColumn(sourceCols, "Email")
                                   ?? ResolveSourceColumn(sourceCols, "EMail");
                    var colPhone    = ResolveSourceColumn(sourceCols, "Telefon")
                                   ?? ResolveSourceColumn(sourceCols, "Phone");

                    AddCol("Code",      colCode  != null && row.TryGetValue(colCode,  out var vCode)  ? vCode  : null);
                    AddCol("FirstName", colFirst != null && row.TryGetValue(colFirst, out var vFirst) ? vFirst : null);
                    AddCol("LastName",  colLast  != null && row.TryGetValue(colLast,  out var vLast)  ? vLast  : null);
                    AddCol("Email",     colEmail != null && row.TryGetValue(colEmail, out var vEmail) ? vEmail : null);
                    AddCol("Phone",     colPhone != null && row.TryGetValue(colPhone, out var vPhone) ? vPhone : null);
                    AddCol("InsertDateTime", row.GetValueOrDefault("InsertDateTime") ?? DateTime.UtcNow);
                    AddCol("UpdateDateTime", row.GetValueOrDefault("UpdateDateTime"));
                    AddCol("DeleteDateTime", row.GetValueOrDefault("DeleteDateTime"));

                    await using var ins = new SqlCommand(
                        $"INSERT INTO common.Personal ({string.Join(",", insertCols.Select(c => $"[{c}]"))}) OUTPUT INSERTED.Id VALUES ({string.Join(",", insertCols.Select((_, i) => $"@p{i}"))})",
                        target);
                    for (var i = 0; i < insertCols.Count; i++)
                        ins.Parameters.AddWithValue($"@p{i}", insertVals[i] ?? DBNull.Value);

                    var newIdObj = await ins.ExecuteScalarAsync(ct);
                    var newId = newIdObj == null || newIdObj == DBNull.Value ? 0 : Convert.ToInt64(newIdObj);
                    if (legacyId > 0 && newId > 0)
                        idMap["PersonelTanim"][legacyId] = newId;

                    result.Inserted++;
                    _runInserted++;
                }
                catch (Exception ex)
                {
                    result.Failed++;
                    _runFailed++;
                    var note = $"FAIL PersonelTanim→common.Personal | LegacyId={row.GetValueOrDefault("Id")} | {ex.GetType().Name}: {ex.Message}";
                    if (result.Errors.Count < 200)
                        result.Errors.Add(note);
                    Report("PersonelTanim", iRow, rows.Count, note);
                }
            }
        }
        catch (Exception ex)
        {
            result.Errors.Add($"PersonelTanim genel hata: {ex.Message}");
            Report("PersonelTanim", 0, 0, $"HATA PersonelTanim: {ex.Message}");
        }

        Report("PersonelTanim", result.SourceCount, result.SourceCount,
            $"PersonelTanim bitti: ins={result.Inserted}, skip={result.Skipped}, fail={result.Failed}");
        return result;
    }

    private void Report(string table, int current, int total, string message)
    {
        _progress?.Report(new TransferProgress
        {
            Phase = "transfer",
            Table = table,
            Current = current,
            Total = total,
            Message = message,
            Inserted = _runInserted,
            Skipped = _runSkipped,
            Failed = _runFailed
        });
    }
}
