using System;

namespace EnKolayCari2026.Application.Model.DTO.Trade
{
    public class TradeDocumentCurrencyDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public long? TradeDocumentId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? Value { get; set; }
        public DateTime? InsertDateTime { get; set; }
        public long? InsertUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? RecordDateTime { get; set; }
    }
}
