using System;

namespace EnKolayCari2026.Application.Model.DTO.Finance
{
    public class AccountDocumentDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public long DocumentDefId { get; set; }
        public string DocumentTanimDescription { get; set; }
        public long AccountId { get; set; }
        public string DocumentIcerik { get; set; }
        public string ApprovalType { get; set; }
        public long InsertUser { get; set; }
        public DateTime InsertDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public DateTime RecordDateTime { get; set; }
    }
}
