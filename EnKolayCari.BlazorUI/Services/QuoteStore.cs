namespace EnKolayCari.BlazorUI.Services;

/// <summary>Scoped in-memory mock store shared between the quote list page and the
/// full-page create/edit form, so a record created or edited on the mockup form shows up back in the grid.</summary>
public sealed class QuoteStore
{
    public sealed record QuoteLine(
        string ProductSku,
        string ProductName,
        decimal Quantity,
        string Unit,
        decimal UnitPrice,
        decimal TaxRate,
        decimal DiscountValue,
        string DiscountType,
        DateTime? DeliveryDate,
        decimal ShippedQuantity);

    public sealed record Quote(
        string QuoteNo,
        string Customer,
        string Date,
        string ValidUntil,
        string AmountLabel,
        string Status,
        string CustomerCode = "",
        bool IsCorporate = true,
        string Address = "",
        string Country = "Türkiye",
        string City = "",
        string District = "",
        string TaxOffice = "",
        string TaxNumber = "",
        bool DifferentDeliveryAddress = false,
        string DeliveryAddress = "",
        string Currency = "TRY",
        string SalesRep = "",
        string PaymentTerm = "",
        string Category = "",
        string Description = "",
        bool PricesIncludeTax = false,
        decimal HeaderDiscountValue = 0,
        string HeaderDiscountType = "Oran",
        IReadOnlyList<QuoteLine>? Lines = null);

    public List<Quote> Quotes { get; } =
    [
        new("TKL-2026-0088", "ABC Ticaret Ltd. Şti.", "05 Eki 2026", "20 Eki 2026", "₺18.400,00", "Beklemede", CustomerCode: "120.001"),
        new("TKL-2026-0087", "Yıldız Elektronik", "03 Eki 2026", "17 Eki 2026", "₺6.250,00", "Onaylandı", CustomerCode: "120.004"),
        new("TKL-2026-0086", "Deniz Yapı Market", "01 Eki 2026", "15 Eki 2026", "₺2.980,00", "Reddedildi", CustomerCode: "120.003"),
        new("TKL-2026-0085", "Güneş Tekstil San.", "28 Eyl 2026", "12 Eki 2026", "₺11.760,00", "Onaylandı", CustomerCode: "120.005"),
        new("TKL-2026-0084", "XYZ Sanayi A.Ş.", "25 Eyl 2026", "09 Eki 2026", "₺34.500,00", "Beklemede", CustomerCode: "120.002"),
    ];

    public Quote Add(Quote quote)
    {
        var quoteNo = string.IsNullOrWhiteSpace(quote.QuoteNo) ? NextQuoteNo() : quote.QuoteNo;
        var record = quote with { QuoteNo = quoteNo };
        Quotes.Insert(0, record);
        return record;
    }

    public Quote? FindByQuoteNo(string quoteNo) =>
        Quotes.FirstOrDefault(q => string.Equals(q.QuoteNo, quoteNo, StringComparison.OrdinalIgnoreCase));

    public string PreviewNextQuoteNo() => NextQuoteNo();

    public void Update(string quoteNo, Quote quote)
    {
        var index = Quotes.FindIndex(q => string.Equals(q.QuoteNo, quoteNo, StringComparison.OrdinalIgnoreCase));
        if (index < 0)
        {
            return;
        }

        Quotes[index] = quote;
    }

    private string NextQuoteNo()
    {
        var nextNumber = Quotes.Count + 89;
        return $"TKL-{DateTime.Now.Year}-{nextNumber:0000}";
    }
}
