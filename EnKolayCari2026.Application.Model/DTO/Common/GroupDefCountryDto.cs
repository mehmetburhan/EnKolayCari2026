using System;

namespace EnKolayCari2026.Application.Model.DTO.Common
{
    public class GroupDefCountryDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public bool Stat { get; set; }
        public long CompanyId { get; set; }
        public long GroupDefId { get; set; }
        public string CountryCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime? ModifedDate { get; set; }
        public long? ModifedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedUser { get; set; }
        public long IsDelete { get; set; }
    }
}
