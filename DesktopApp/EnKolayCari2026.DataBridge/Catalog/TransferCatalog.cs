using System.IO;
using Newtonsoft.Json;

namespace EnKolayCari2026.DataBridge.Catalog;

public sealed class TransferCatalogRoot
{
    public List<TransferTableDef> Tables { get; set; } = new();
}

public sealed class TransferTableDef
{
    public string LegacyTable { get; set; } = "";
    public string SourceDb { get; set; } = "";
    public string NewSchema { get; set; } = "";
    public string NewTable { get; set; } = "";
    public string Entity { get; set; } = "";
    public bool CompanyFilter { get; set; }
    public int Order { get; set; }
    public List<TransferColumnDef> Columns { get; set; } = new();
    public List<TransferFkDef> Fks { get; set; } = new();

    [JsonIgnore]
    public string QualifiedNewTable => $"[{NewSchema}].[{NewTable}]";
}

public sealed class TransferColumnDef
{
    public string New { get; set; } = "";
    public string Legacy { get; set; } = "";
    public string Role { get; set; } = "data";
    public string? FkTable { get; set; }
}

public sealed class TransferFkDef
{
    public string NewCol { get; set; } = "";
    public string LegacyCol { get; set; } = "";
    public string LegacyTable { get; set; } = "";
}

public static class TransferCatalogLoader
{
    public static TransferCatalogRoot Load(string? baseDir = null)
    {
        var dir = baseDir ?? AppContext.BaseDirectory;
        var path = Path.Combine(dir, "Catalog", "transfer-catalog.json");
        if (!File.Exists(path))
            throw new FileNotFoundException("transfer-catalog.json bulunamadı.", path);
        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<TransferCatalogRoot>(json)
               ?? new TransferCatalogRoot();
    }
}
