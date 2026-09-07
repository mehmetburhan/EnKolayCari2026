using System;

namespace EnKolayCari.Application.Model.DTO.Trade
{
    public class TradeDocumentTempDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public Guid PersonalGId { get; set; }
        public int HareketType { get; set; }
        public long? AccountId { get; set; }
        public string TradeDocumentNo { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string CurrencyCode { get; set; }
        public string Address { get; set; }
    }
}
