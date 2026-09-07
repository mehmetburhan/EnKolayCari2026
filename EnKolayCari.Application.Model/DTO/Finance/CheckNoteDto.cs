using System;

namespace EnKolayCari.Application.Model.DTO.Finance
{
    public class CheckNoteDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public long AccountId { get; set; }
        public long KimdeAccountDefId { get; set; }
        public string DocumentNo { get; set; }
        public int DocumentStatus { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime DueDate { get; set; }
        public int HareketType { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string DocumentOriginalOwnerTitle { get; set; }
        public string TCKN { get; set; }
        public string OwningBank { get; set; }
        public string BankBranch { get; set; }
        public string KesideYeri { get; set; }
        public string IBAN { get; set; }
        public string MersisNumber { get; set; }
        public string FindeksNo { get; set; }
        public string Label { get; set; }
        public DateTime InsertDatetime { get; set; }
        public long InsertUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime RecordDateTime { get; set; }
    }
}
