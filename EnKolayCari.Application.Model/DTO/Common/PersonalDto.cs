using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class PersonalDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public string CompanyMasterCode { get; set; }
        public bool Stat { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public bool UseEMailForLogin { get; set; }
        public string Email { get; set; }
        public string DomainUserId { get; set; }
        public bool IsWebEnabled { get; set; }
        public bool IsBranchManager { get; set; }
        public string ManagerAccountCode { get; set; }
        public string CostCenter { get; set; }
        public string PhoneNumber { get; set; }
        public long? BranchId { get; set; }
        public long? ParentPersonalId { get; set; }
        public string IdentityNumber { get; set; }
        public string ManagerIdentityNumber { get; set; }
        public DateTime? StartWorkDate { get; set; }
        public DateTime? EndWorkDate { get; set; }
        public string AppCode { get; set; }
        public long? AppCodeId { get; set; }
        public string CountryCode { get; set; }
        public string UserId { get; set; }
        public string UserIdForDomain { get; set; }
        public string DeviceId { get; set; }
        public string CustomClientOs { get; set; }
        public string CustomClientAppVersion { get; set; }
        public string Color { get; set; }
        public string ResetPassCode { get; set; }
        public DateTime? ResetPassExpireDate { get; set; }
        public string EmplId { get; set; }
        public string Division { get; set; }
        public string PositionDesc { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime? ModifedDate { get; set; }
        public long? ModifedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedUser { get; set; }
        public long IsDelete { get; set; }
    }
}
