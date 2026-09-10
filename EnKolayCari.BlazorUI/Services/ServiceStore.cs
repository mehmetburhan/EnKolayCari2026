namespace EnKolayCari.BlazorUI.Services;

/// <summary>Scoped in-memory mock store shared between the service list page and the
/// full-page create/edit form, so a record created or edited on the mockup form shows up back in the grid.</summary>
public sealed class ServiceStore
{
    public sealed record Feature(string Name, string Type, string Value, string Status);

    public sealed record DefenseIndustryItem(string LocalizationRate, string Party, string OrderItemNumber, string OrderItemDescription);

    public sealed record Service(
        string Code,
        string Name,
        string Category,
        string Unit,
        string PriceLabel,
        string Status,
        string Description = "",
        string ServiceGroup = "",
        string ExpenseType = "",
        string Currency = "TRY",
        string SalePriceIncludingVat = "",
        string SalePriceExcludingVat = "",
        string PurchasePriceIncludingVat = "",
        string PurchasePriceExcludingVat = "",
        string WithholdingType = "",
        string WithholdingRate = "",
        string AdditionalTax = "",
        IReadOnlyList<DefenseIndustryItem>? DefenseIndustryItems = null,
        IReadOnlyList<Feature>? Features = null);

    public List<Service> Services { get; } =
    [
        new("HZM-001", "Web Sitesi Bakım Hizmeti", "Danışmanlık", "Ay", "₺2.500,00", "Aktif", ServiceGroup: "Bakım & Destek", ExpenseType: "Genel Gider", SalePriceIncludingVat: "2.500,00", SalePriceExcludingVat: "2.083,33"),
        new("HZM-002", "Muhasebe Danışmanlığı", "Danışmanlık", "Saat", "₺450,00", "Aktif", ServiceGroup: "Danışmanlık", ExpenseType: "Genel Gider", SalePriceIncludingVat: "450,00", SalePriceExcludingVat: "375,00"),
        new("HZM-003", "Kurulum & Montaj", "Teknik Servis", "Adet", "₺750,00", "Aktif", ServiceGroup: "Kurulum", ExpenseType: "Personel", SalePriceIncludingVat: "750,00", SalePriceExcludingVat: "625,00"),
        new("HZM-004", "Yazılım Lisans Desteği", "Yazılım", "Yıl", "₺6.000,00", "Aktif", ServiceGroup: "Bakım & Destek", ExpenseType: "Genel Gider", SalePriceIncludingVat: "6.000,00", SalePriceExcludingVat: "5.000,00"),
        new("HZM-005", "Nakliye Hizmeti", "Lojistik", "Sefer", "₺1.200,00", "Aktif", ServiceGroup: "Lojistik", ExpenseType: "Lojistik", SalePriceIncludingVat: "1.200,00", SalePriceExcludingVat: "1.000,00"),
        new("HZM-006", "Eğitim & Seminer", "Danışmanlık", "Gün", "₺3.800,00", "Pasif", ServiceGroup: "Eğitim", ExpenseType: "Pazarlama", SalePriceIncludingVat: "3.800,00", SalePriceExcludingVat: "3.166,67"),
    ];

    public Service Add(Service service)
    {
        var code = string.IsNullOrWhiteSpace(service.Code) ? NextCode() : service.Code;
        var saved = service with { Code = code };
        Services.Insert(0, saved);
        return saved;
    }

    public Service? FindByCode(string code) =>
        Services.FirstOrDefault(s => string.Equals(s.Code, code, StringComparison.OrdinalIgnoreCase));

    public void Update(string code, Service service)
    {
        var index = Services.FindIndex(s => string.Equals(s.Code, code, StringComparison.OrdinalIgnoreCase));
        if (index < 0)
        {
            return;
        }

        Services[index] = service;
    }

    private string NextCode()
    {
        var nextNumber = Services.Count + 1;
        return $"HZM-{nextNumber:000}";
    }
}
