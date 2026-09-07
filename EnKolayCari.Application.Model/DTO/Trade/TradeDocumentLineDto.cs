using System;

namespace EnKolayCari.Application.Model.DTO.Trade
{
    public class TradeDocumentLineDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public long TradeDocumentId { get; set; }
        public long StoreId { get; set; }
        public int HareketType { get; set; }
        public long ProductId { get; set; }
        public string ProductDescription { get; set; }
        public string SeriNo { get; set; }
        public string ColorSize { get; set; }
        public decimal Quantity { get; set; }
        public long UnitId { get; set; }
        public decimal? UnitKatsayi { get; set; }
        public decimal UnitPrice { get; set; }
        public string CurrencyCode { get; set; }
        public int VatRate { get; set; }
        public decimal VatTutari { get; set; }
        public decimal SatirTutari { get; set; }
        public bool? StokSayimiDahilEtme { get; set; }
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
        public decimal? VadeFarkiPercent { get; set; }
    }
}
