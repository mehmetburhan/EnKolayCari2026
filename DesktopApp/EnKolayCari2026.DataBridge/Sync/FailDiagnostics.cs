using System.Text;
using Microsoft.Data.SqlClient;

namespace EnKolayCari2026.DataBridge.Sync;

/// <summary>
/// Fail loglarına tablo/satır bağlamı + olası çözüm ipuçları ekler.
/// </summary>
public static class FailDiagnostics
{
    public static string Format(
        string legacyTable,
        string newTable,
        object? legacyId,
        Exception ex,
        IReadOnlyDictionary<string, object?>? rowSample = null)
    {
        var sb = new StringBuilder();
        sb.Append("FAIL ");
        sb.Append(legacyTable);
        sb.Append(" → ");
        sb.Append(newTable);
        if (legacyId != null)
            sb.Append(" | LegacyId=").Append(legacyId);
        sb.Append(" | ").Append(ex.GetType().Name).Append(": ").Append(ex.Message);

        if (ex is SqlException sql)
        {
            sb.Append(" | SqlNumber=").Append(sql.Number);
            if (!string.IsNullOrWhiteSpace(sql.Procedure))
                sb.Append(" Proc=").Append(sql.Procedure);
            var hint = HintForSqlNumber(sql.Number, sql.Message, legacyTable, newTable);
            if (hint != null)
                sb.Append(" | ÇÖZÜM: ").Append(hint);
        }
        else
        {
            var hint = HintForMessage(ex.Message, legacyTable, newTable);
            if (hint != null)
                sb.Append(" | ÇÖZÜM: ").Append(hint);
        }

        if (rowSample is { Count: > 0 })
        {
            var parts = new List<string>();
            foreach (var key in new[] { "Id", "GId", "Kod", "Code", "CariKodu", "UrunKodu", "FaturaNo", "SirketTanimId" })
            {
                if (rowSample.TryGetValue(key, out var v) && v != null)
                    parts.Add($"{key}={Trim(v)}");
            }
            if (parts.Count > 0)
                sb.Append(" | Satır: ").Append(string.Join(", ", parts));
        }

        if (ex.InnerException != null)
            sb.Append(" | Inner: ").Append(ex.InnerException.Message);

        return sb.ToString();
    }

    private static string? HintForSqlNumber(int number, string message, string legacyTable, string newTable) =>
        number switch
        {
            515 => "NOT NULL kolona null geldi. Kaynakta boş alan var veya kolon eşlemesi eksik; hedef şemada zorunlu alanı ve katalog map’ini kontrol et.",
            547 => "FK ihlali. Önce parent tablo aktarılmalı veya remap (LegacyTransferMap/idMap) başarısız; bağımlı Id hedefte yok.",
            2627 or 2601 => "Unique/PK çakışması. Aynı GId veya benzersiz alan zaten var; SkipExisting açık mı, yoksa hedefte eski deneme satırlarını temizle.",
            207 => $"Geçersiz kolon adı. Hedef {newTable} ile katalog kolon listesi uyuşmuyor; EF generate / INFORMATION_SCHEMA ile karşılaştır.",
            208 => $"Tablo/nesne yok: {newTable}. EKCN2026 şeması kurulmuş mu? (FullSchema.sql)",
            245 => "Tip dönüşümü hatası (string→sayı/tarih). Kaynak değer formatını kontrol et.",
            8114 or 8115 => "Numeric overflow / convert. Decimal/precision veya fazla uzun string; MaxLength aşımı olabilir.",
            8152 => "String çok uzun (MaxLength). Kaynak metni kısalt veya hedef nvarchar boyutunu artır.",
            _ => HintForMessage(message, legacyTable, newTable)
        };

    private static string? HintForMessage(string message, string legacyTable, string newTable)
    {
        var m = message ?? "";
        if (m.Contains("GId", StringComparison.OrdinalIgnoreCase) && m.Contains("Invalid column", StringComparison.OrdinalIgnoreCase))
            return $"{newTable} tablosunda GId yok; stable-guid / map yolu kullanılmalı (güncel DataBridge sürümü).";
        if (m.Contains("eşleşen kolon", StringComparison.OrdinalIgnoreCase))
            return $"Katalogda {legacyTable} kolon map’i boş veya kaynak/hedef kolon adları farklı. gen_transfer_catalog.py yenile.";
        if (m.Contains("Cannot insert the value NULL", StringComparison.OrdinalIgnoreCase))
            return "Zorunlu alan null. Satır örneğindeki boş kolonları ve map role=company/fk değerlerini kontrol et.";
        if (m.Contains("PRIMARY KEY", StringComparison.OrdinalIgnoreCase) || m.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase))
            return "Tekrarlayan kayıt. GId skip veya hedef satırı silip yeniden dene.";
        if (m.Contains("timeout", StringComparison.OrdinalIgnoreCase))
            return "Timeout. Batch küçült, ağ/VPN kontrol et, büyük tabloları ayrı çalıştır.";
        if (m.Contains("login failed", StringComparison.OrdinalIgnoreCase))
            return "SQL login başarısız. appsettings / UI bağlantı bilgilerini doğrula.";
        if (m.Contains("SqlDateTime overflow", StringComparison.OrdinalIgnoreCase) ||
            m.Contains("between 1/1/1753", StringComparison.OrdinalIgnoreCase))
            return "Tarih SQL datetime aralığı dışında (ör. 0001-01-01 soft-delete). DataBridge geçersiz tarihleri null’a çeker; uygulamayı güncelleyip yeniden dene.";
        if (m.Contains("PersonalId", StringComparison.OrdinalIgnoreCase) && m.Contains("NULL", StringComparison.OrdinalIgnoreCase))
            return "common.Personal henüz yok veya PersonelTanim remap başarısız. Personel aktarımı Token’dan önce çalışmalı.";
        if (m.Contains("RecordSource", StringComparison.OrdinalIgnoreCase))
            return "Kaynak kolon KayıtYeri (Türkçe ı) olabilir veya null; varsayılan 0 uygulanmalı.";
        return null;
    }

    private static string Trim(object v)
    {
        var s = Convert.ToString(v) ?? "";
        return s.Length <= 40 ? s : s[..40] + "…";
    }
}
