using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class PersonalGroupDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long? CompanyId { get; set; }
        public long PersonalId { get; set; }
        public int GroupCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime? ModifedDate { get; set; }
        public long? ModifedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedUser { get; set; }
        public long IsDelete { get; set; }
    }
}
