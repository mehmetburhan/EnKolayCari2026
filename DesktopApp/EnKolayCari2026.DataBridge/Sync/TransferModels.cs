namespace EnKolayCari2026.DataBridge.Sync;

public sealed class CompanyListItem
{
    public long LegacyId { get; set; }
    public Guid GId { get; set; }
    public string Code { get; set; } = "";
    public string Title { get; set; } = "";
    public bool Stat { get; set; }

    public string Display => $"{Code} — {Title} (Id:{LegacyId})";
}

public sealed class TransferProgress
{
    public string Phase { get; set; } = "";
    public string Table { get; set; } = "";
    public int Current { get; set; }
    public int Total { get; set; }
    public string Message { get; set; } = "";
    /// <summary>Aktarım boyunca biriken toplamlar (tablo ayrımı yok).</summary>
    public int Inserted { get; set; }
    public int Skipped { get; set; }
    public int Failed { get; set; }
}

public sealed class TableTransferResult
{
    public string LegacyTable { get; set; } = "";
    public string NewTable { get; set; } = "";
    public int SourceCount { get; set; }
    public int Inserted { get; set; }
    public int Skipped { get; set; }
    public int Failed { get; set; }
    public List<string> Errors { get; set; } = new();
}

public sealed class TransferRunResult
{
    public DateTime StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public long LegacyCompanyId { get; set; }
    public long NewCompanyId { get; set; }
    public List<TableTransferResult> Tables { get; set; } = new();
    public List<string> Errors { get; set; } = new();
    public bool Cancelled { get; set; }

    public int TotalInserted => Tables.Sum(t => t.Inserted);
    public int TotalSkipped => Tables.Sum(t => t.Skipped);
    public int TotalFailed => Tables.Sum(t => t.Failed) + Errors.Count;
}

public sealed class CleanResult
{
    public long LegacyCompanyId { get; set; }
    public long TargetCompanyId { get; set; }
    /// <summary>Silinen satır sayıları tablo bazında.</summary>
    public Dictionary<string, int> DeletedRows { get; set; } = new();
    public List<string> Errors { get; set; } = new();
    public List<string> Warnings { get; set; } = new();

    public int TotalDeleted => DeletedRows.Values.Sum();
    public bool HasErrors => Errors.Count > 0;
}
