using System;

namespace EnKolayCari2026.Application.Model.DTO.Trade
{
    public class TradeDocumentLineDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public long TradeDocumentId { get; set; }
        public long StoreId { get; set; }
        public int DocumentType { get; set; }
        public long ProductId { get; set; }
        public string ProductDescription { get; set; }
        public string SerialNo { get; set; }
        public string ColorSize { get; set; }
        public decimal Quantity { get; set; }
        public long UnitId { get; set; }
        public decimal? UnitMultiplier { get; set; }
        public decimal UnitPrice { get; set; }
        public string CurrencyCode { get; set; }
        public int VatRate { get; set; }
        public decimal VatAmount { get; set; }
        public decimal LineAmount { get; set; }
        public bool? ExcludeFromStockCount { get; set; }
        public string Label { get; set; }
        public DateTime? InsertDateTime { get; set; }
        public long? InsertUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? RecordDateTime { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? Discount1Percent { get; set; }
        public decimal? DeferralPercent { get; set; }
    }
}
