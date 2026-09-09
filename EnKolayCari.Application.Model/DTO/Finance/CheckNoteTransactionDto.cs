using System;

namespace EnKolayCari.Application.Model.DTO.Finance
{
    public class CheckNoteTransactionDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public DateTime TransactionDate { get; set; }
        public long CompanyId { get; set; }
        public long CheckNoteId { get; set; }
        public int DocumentStatus { get; set; }
        public long AccountId { get; set; }
        public long AccountTransactionId { get; set; }
        public long CashRegisterTransactionId { get; set; }
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
