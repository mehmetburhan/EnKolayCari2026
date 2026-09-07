using System;

namespace EnKolayCari.Application.Model.DTO.Inventory
{
    public class ProductBarcodeDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public long ProductId { get; set; }
        public int SortOrder { get; set; }
        public bool IsMainProduct { get; set; }
        public string ColorSize { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public long? ProductColorPaletteId { get; set; }
        public string Barcode { get; set; }
        public bool IsSalePriceActive { get; set; }
        public decimal SalePrice { get; set; }
        public decimal InstallmentSalePrice { get; set; }
        public decimal? PreviousSalePrice { get; set; }
        public long InsertUser { get; set; }
        public DateTime InsertDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public DateTime RecordDateTime { get; set; }
        public string HepsiBuradaProductId { get; set; }
    }
}
