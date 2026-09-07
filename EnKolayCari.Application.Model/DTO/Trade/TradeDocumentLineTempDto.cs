using System;

namespace EnKolayCari.Application.Model.DTO.Trade
{
    public class TradeDocumentLineTempDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public Guid PersonalGId { get; set; }
        public long? SepetId { get; set; }
        public long CompanyId { get; set; }
        public int HareketType { get; set; }
        public Guid ProductGId { get; set; }
        public string ProductDescription { get; set; }
        public string SeriNo { get; set; }
        public string ColorSize { get; set; }
        public decimal Quantity { get; set; }
        public long UnitId { get; set; }
        public decimal? UnitKatsayi { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceVatIncluded { get; set; }
        public string CurrencyCode { get; set; }
        public int VatRate { get; set; }
        public string VatDH { get; set; }
        public decimal? ToplamTutarVatHaric { get; set; }
        public decimal ToplamTutar { get; set; }
        public Guid? TradeDocumentGId { get; set; }
        public Guid? TradeDocumentHareketGId { get; set; }
        public DateTime InsertDateTime { get; set; }
        public string EcommercePaymentId { get; set; }
        public bool Tukenmis { get; set; }
        public Guid? StoreTanimGId { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? Discount1Percent { get; set; }
        public decimal? VadeFarkiPercent { get; set; }
    }
}
