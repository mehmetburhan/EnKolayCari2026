namespace EnKolayCari.BlazorUI.Services;

/// <summary>Scoped in-memory mock store shared between the warehouse list page and the
/// full-page create/edit form, so a record created or edited on the mockup form shows up back in the grid.</summary>
public sealed class WarehouseStore
{
    public sealed record Warehouse(
        string Code,
        string Name,
        string City,
        string Responsible,
        string OccupancyLabel,
        string Status,
        string Type = "",
        string District = "",
        string Address = "",
        string PostalCode = "",
        string Phone = "",
        string Email = "",
        bool IsDefault = false,
        string Capacity = "",
        string Notes = "");

    public List<Warehouse> Warehouses { get; } =
    [
        new("DPO-01", "Merkez Depo", "İstanbul", "Ahmet Yılmaz", "%78", "Aktif", Type: "Merkez Depo", District: "Ümraniye", IsDefault: true, Capacity: "5.000 m²"),
        new("DPO-02", "Anadolu Yakası Depo", "İstanbul", "Elif Kaya", "%52", "Aktif", Type: "Şube Deposu", District: "Kartal", Capacity: "2.200 m²"),
        new("DPO-03", "Ege Bölge Depo", "İzmir", "Mert Demir", "%34", "Aktif", Type: "Şube Deposu", District: "Bornova", Capacity: "1.800 m²"),
        new("DPO-04", "Marmara Lojistik Merkezi", "Kocaeli", "Selin Arslan", "%91", "Aktif", Type: "Transit Depo", District: "Gebze", Capacity: "6.500 m²"),
        new("DPO-05", "Yedek Parça Deposu", "Bursa", "Can Öztürk", "%12", "Pasif", Type: "Fire Deposu", District: "Nilüfer", Capacity: "600 m²"),
    ];

    public Warehouse Add(Warehouse warehouse)
    {
        var code = string.IsNullOrWhiteSpace(warehouse.Code) ? NextCode() : warehouse.Code;
        var record = warehouse with { Code = code };
        Warehouses.Insert(0, record);
        return record;
    }

    public Warehouse? FindByCode(string code) =>
        Warehouses.FirstOrDefault(w => string.Equals(w.Code, code, StringComparison.OrdinalIgnoreCase));

    public void Update(string code, Warehouse warehouse)
    {
        var index = Warehouses.FindIndex(w => string.Equals(w.Code, code, StringComparison.OrdinalIgnoreCase));
        if (index < 0)
        {
            return;
        }

        Warehouses[index] = warehouse;
    }

    private string NextCode()
    {
        var nextNumber = Warehouses.Count + 1;
        return $"DPO-{nextNumber:00}";
    }
}
