using System;

namespace EnKolayCari.Application.Model.DTO.Inventory
{
    public class StockCountLineDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public long InsertUser { get; set; }
        public DateTime InsertDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public DateTime RecordDateTime { get; set; }
        public long StockCountId { get; set; }
        public long ProductId { get; set; }
        public string Color { get; set; }
        public string Barcode { get; set; }
        public decimal Quantity { get; set; }
        public long TradeDocumentLineId { get; set; }
    }
}
