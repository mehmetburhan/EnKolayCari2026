using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class CentralCurrencyRateDto
    {
        public long Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Value { get; set; }
        public string DefaultCurrencyCode { get; set; }
        public DateTime? InsertDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
