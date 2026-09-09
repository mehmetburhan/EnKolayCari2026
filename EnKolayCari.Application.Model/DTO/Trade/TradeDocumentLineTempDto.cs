using System;

namespace EnKolayCari.Application.Model.DTO.Trade
{
    public class TradeDocumentLineTempDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public Guid PersonalGId { get; set; }
        public long? CartId { get; set; }
        public long CompanyId { get; set; }
        public int DocumentType { get; set; }
        public Guid ProductGId { get; set; }
        public string ProductDescription { get; set; }
        public string SerialNo { get; set; }
        public string ColorSize { get; set; }
        public decimal Quantity { get; set; }
        public long UnitId { get; set; }
        public decimal? UnitMultiplier { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceVatIncluded { get; set; }
        public string CurrencyCode { get; set; }
        public int VatRate { get; set; }
        public string VatDH { get; set; }
        public decimal? TotalAmountExVat { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid? TradeDocumentGId { get; set; }
        public Guid? TradeDocumentTypeGId { get; set; }
        public DateTime InsertDateTime { get; set; }
        public string EcommercePaymentId { get; set; }
        public bool IsSoldOut { get; set; }
        public Guid? StoreGId { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? Discount1Percent { get; set; }
        public decimal? DeferralPercent { get; set; }
    }
}
