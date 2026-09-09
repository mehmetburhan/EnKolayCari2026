using System;

namespace EnKolayCari.Application.Model.DTO.Finance
{
    public class CashTransactionDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public long CashRegisterId { get; set; }
        public long AccountTransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int CashRegisterTransactionType { get; set; }
        public string Description { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string CurrencyCode { get; set; }
        public string Label { get; set; }
        public DateTime InsertDateTime { get; set; }
        public long InsertUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? RecordDateTime { get; set; }
    }
}
