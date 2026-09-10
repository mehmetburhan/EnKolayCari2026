namespace EnKolayCari.BlazorUI.Services;

/// <summary>Scoped in-memory mock store shared between the customer/supplier list page and the
/// full-page create form, so a record created on the mockup form shows up back in the grid.</summary>
public sealed class CustomerSupplierStore
{
    public sealed record Account(
        string Code,
        string Name,
        string Type,
        string City,
        string BalanceLabel,
        string Status,
        string NationalId = "",
        string TaxNumber = "",
        string TaxOffice = "");

    public List<Account> Accounts { get; } =
    [
        new("120.001", "ABC Ticaret Ltd. Şti.", "Müşteri", "İstanbul", "₺24.560,75", "Aktif", TaxNumber: "1234567890", TaxOffice: "Kadıköy"),
        new("120.002", "XYZ Sanayi A.Ş.", "Müşteri", "Bursa", "₺11.500,00", "Aktif", TaxNumber: "2345678901", TaxOffice: "Osmangazi"),
        new("320.001", "Mavi Tedarik Ltd.", "Tedarikçi", "Ankara", "-₺3.205,00", "Aktif", TaxNumber: "3456789012", TaxOffice: "Çankaya"),
        new("120.003", "Deniz Yapı Market", "Müşteri", "İzmir", "₺890,00", "Pasif", TaxNumber: "4567890123", TaxOffice: "Konak"),
        new("320.002", "Kartal Lojistik A.Ş.", "Tedarikçi", "Kocaeli", "-₺7.420,50", "Aktif", TaxNumber: "5678901234", TaxOffice: "İzmit"),
        new("120.004", "Yıldız Elektronik", "Müşteri", "Antalya", "₺15.230,00", "Aktif", TaxNumber: "6789012345", TaxOffice: "Muratpaşa"),
        new("320.003", "Orkide Kırtasiye", "Tedarikçi", "İstanbul", "-₺459,90", "Aktif", TaxNumber: "7890123456", TaxOffice: "Şişli"),
        new("120.005", "Güneş Tekstil San.", "Müşteri", "Denizli", "₺6.780,40", "Pasif", TaxNumber: "8901234567", TaxOffice: "Merkezefendi"),
    ];

    public Account Add(string name, string typeLabel, string city, string status, string nationalId = "", string taxNumber = "", string taxOffice = "")
    {
        var prefix = typeLabel switch
        {
            "Tedarikçi" => "320",
            "Müşteri & Tedarikçi" => "126",
            _ => "120",
        };

        var nextNumber = Accounts.Count(a => a.Code.StartsWith(prefix, StringComparison.Ordinal)) + 1;
        var code = $"{prefix}.{nextNumber:000}";

        var account = new Account(code, name, typeLabel, string.IsNullOrWhiteSpace(city) ? "-" : city, "₺0,00", status, nationalId, taxNumber, taxOffice);
        Accounts.Insert(0, account);
        return account;
    }

    public Account? FindByCode(string code) =>
        Accounts.FirstOrDefault(a => string.Equals(a.Code, code, StringComparison.OrdinalIgnoreCase));

    public void Update(string code, string name, string typeLabel, string city, string status, string nationalId = "", string taxNumber = "", string taxOffice = "")
    {
        var index = Accounts.FindIndex(a => string.Equals(a.Code, code, StringComparison.OrdinalIgnoreCase));
        if (index < 0)
        {
            return;
        }

        Accounts[index] = Accounts[index] with
        {
            Name = name,
            Type = typeLabel,
            City = string.IsNullOrWhiteSpace(city) ? "-" : city,
            Status = status,
            NationalId = nationalId,
            TaxNumber = taxNumber,
            TaxOffice = taxOffice,
        };
    }
}
