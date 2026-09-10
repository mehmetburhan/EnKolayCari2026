namespace EnKolayCari.BlazorUI.Services;

/// <summary>Scoped in-memory mock store shared between the product list page and the
/// full-page create/edit form, so a record created or edited on the mockup form shows up back in the grid.</summary>
public sealed class ProductStore
{
    public sealed record Feature(string Name, string Type, string Value, string Status);

    public sealed record Product(
        string Sku,
        string Name,
        string Category,
        string Unit,
        int StockQty,
        string PriceLabel,
        string Status,
        string ProductCode = "",
        string Barcode = "",
        string Name2 = "",
        string Description = "",
        string Brand = "",
        string Model = "",
        int CriticalStockQty = 0,
        int MinSaleQty = 0,
        string Currency = "TRY",
        string SalePriceIncludingVat = "",
        string SalePriceExcludingVat = "",
        string PurchasePriceIncludingVat = "",
        string PurchasePriceExcludingVat = "",
        string WithholdingType = "",
        string WithholdingRate = "",
        string AdditionalTax = "",
        IReadOnlyList<Feature>? Features = null);

    public List<Product> Products { get; } =
    [
        new("URN-1001", "A4 Fotokopi Kağıdı 80gr", "Kırtasiye", "Koli", 240, "₺185,00", "Aktif", ProductCode: "P-1001", Brand: "Nero", SalePriceIncludingVat: "185,00", SalePriceExcludingVat: "154,17"),
        new("URN-1002", "Dell 24\" Monitör", "Elektronik", "Adet", 18, "₺4.250,00", "Aktif", ProductCode: "P-1002", Brand: "Dell", Model: "P2422H", SalePriceIncludingVat: "4.250,00", SalePriceExcludingVat: "3.541,67"),
        new("320.001", "Ofis Sandalyesi Ergonomik", "Mobilya", "Adet", 6, "₺1.890,00", "Aktif", ProductCode: "P-1003", SalePriceIncludingVat: "1.890,00", SalePriceExcludingVat: "1.575,00"),
        new("URN-1004", "USB-C Şarj Kablosu 1m", "Elektronik", "Adet", 120, "₺95,00", "Aktif", ProductCode: "P-1004", SalePriceIncludingVat: "95,00", SalePriceExcludingVat: "79,17"),
        new("URN-1005", "Toner HP 26A", "Kırtasiye", "Kutu", 32, "₺620,00", "Aktif", ProductCode: "P-1005", Brand: "HP", SalePriceIncludingVat: "620,00", SalePriceExcludingVat: "516,67"),
        new("URN-1006", "Beyaz Pamuklu Tişört", "Tekstil", "Adet", 0, "₺79,90", "Tükendi", ProductCode: "P-1006", SalePriceIncludingVat: "79,90", SalePriceExcludingVat: "66,58"),
        new("URN-1007", "Laptop Çantası 15.6\"", "Aksesuar", "Adet", 45, "₺340,00", "Aktif", ProductCode: "P-1007", SalePriceIncludingVat: "340,00", SalePriceExcludingVat: "283,33"),
        new("URN-1008", "Endüstriyel Raf Sistemi", "Depo Ekipmanı", "Set", 4, "₺2.750,00", "Pasif", ProductCode: "P-1008", SalePriceIncludingVat: "2.750,00", SalePriceExcludingVat: "2.291,67"),
    ];

    public Product Add(Product product)
    {
        var sku = string.IsNullOrWhiteSpace(product.Sku) ? NextSku() : product.Sku;
        var account = product with { Sku = sku };
        Products.Insert(0, account);
        return account;
    }

    public Product? FindBySku(string sku) =>
        Products.FirstOrDefault(p => string.Equals(p.Sku, sku, StringComparison.OrdinalIgnoreCase));

    public void Update(string sku, Product product)
    {
        var index = Products.FindIndex(p => string.Equals(p.Sku, sku, StringComparison.OrdinalIgnoreCase));
        if (index < 0)
        {
            return;
        }

        Products[index] = product with { StockQty = Products[index].StockQty };
    }

    private string NextSku()
    {
        var nextNumber = Products.Count + 1001;
        return $"URN-{nextNumber}";
    }
}
