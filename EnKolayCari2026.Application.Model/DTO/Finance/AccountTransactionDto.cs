using System;

namespace EnKolayCari2026.Application.Model.DTO.Finance
{
    public class AccountTransactionDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public long AccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string DocumentNo { get; set; }
        public int HareketType { get; set; }
        public long TradeDocumentId { get; set; }
        public string Description { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string CurrencyCode { get; set; }
        public long? RelatedAccountTransactionId { get; set; }
        public long? CheckNoteDefId { get; set; }
        public long? CashRegisterId { get; set; }
        public string Label { get; set; }
        public DateTime InsertDateTime { get; set; }
        public long? InsertUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public DateTime? RecordDateTime { get; set; }
    }
}
