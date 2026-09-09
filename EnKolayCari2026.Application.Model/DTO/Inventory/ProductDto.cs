using System;

namespace EnKolayCari2026.Application.Model.DTO.Inventory
{
    public class ProductDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public bool Stat { get; set; }
        public string ProductCode { get; set; }
        public string OzelKod { get; set; }
        public long BrandId { get; set; }
        public string ProductDescription { get; set; }
        public string ProductKisaBilgi { get; set; }
        public string Barcode { get; set; }
        public long? ColorId { get; set; }
        public bool IsWeightBarcode { get; set; }
        public long UnitId { get; set; }
        public long? CategoryId { get; set; }
        public decimal? CriticalStockQuantity { get; set; }
        public decimal? InternetCriticalStockQuantity { get; set; }
        public bool SerialNumberTracking { get; set; }
        public int PurchaseVatRate { get; set; }
        public int VatRate { get; set; }
        public decimal PurchasePrice { get; set; }
        public string PurchaseCurrencyCode { get; set; }
        public string PurchaseVatIncluded { get; set; }
        public decimal PreviousSalePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal InstallmentSalePrice { get; set; }
        public string SaleCurrencyCode { get; set; }
        public string SaleVatIncluded { get; set; }
        public string ProductDetay { get; set; }
        public string VideoUrl { get; set; }
        public string IntegrationName { get; set; }
        public long? IntegrationId { get; set; }
        public bool IsInternetSaleActive { get; set; }
        public bool IsTrendyolActive { get; set; }
        public bool IsHepsiBuradaActive { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }
        public decimal DesiWeight { get; set; }
        public decimal Weight { get; set; }
        public string Color { get; set; }
        public bool? IsAppointmentActive { get; set; }
        public bool? IsAppointmentOpen { get; set; }
        public string TrendyolSyncType { get; set; }
        public string HepsiBuradaSyncType { get; set; }
        public DateTime? InsertDateTime { get; set; }
        public long? InsertUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? RecordDateTime { get; set; }
        public string HepsiBuradaProductId { get; set; }
        public int MinSaleQuantity { get; set; }
    }
}
