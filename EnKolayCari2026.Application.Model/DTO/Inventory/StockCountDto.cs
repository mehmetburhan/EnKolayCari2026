using System;

namespace EnKolayCari2026.Application.Model.DTO.Inventory
{
    public class StockCountDto
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
        public DateTime StockCountDate { get; set; }
        public string StockCountDescription { get; set; }
        public int StockCountStatus { get; set; }
        public long StoreId { get; set; }
        public long InboundTradeDocumentId { get; set; }
        public long OutboundTradeDocumentId { get; set; }
    }
}
