using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class BranchDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public bool Stat { get; set; }
        public string CompanyMasterCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public long? ManagerPersonalId { get; set; }
        public string ManagerAccountCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string CityCode { get; set; }
        public string DistrictCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string StartIP { get; set; }
        public string EndIP { get; set; }
        public string Color { get; set; }
        public string Label { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime? ModifedDate { get; set; }
        public long? ModifedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedUser { get; set; }
        public long IsDelete { get; set; }
    }
}
