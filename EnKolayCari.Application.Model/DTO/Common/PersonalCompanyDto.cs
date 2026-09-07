using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class PersonalCompanyDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long PersonalId { get; set; }
        public long CompanyId { get; set; }
        public bool IsWebLogin { get; set; }
        public bool IsMobileLogin { get; set; }
        public bool ShowAllAuditSession { get; set; }
        public bool ShowAllAuditAction { get; set; }
        public bool BranchActionReport { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime? ModifedDate { get; set; }
        public long? ModifedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedUser { get; set; }
        public long IsDelete { get; set; }
    }
}
