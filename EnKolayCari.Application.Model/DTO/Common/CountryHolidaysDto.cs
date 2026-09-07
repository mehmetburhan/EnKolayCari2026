using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class CountryHolidaysDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public bool Stat { get; set; }
        public string CountryCode { get; set; }
        public string AppCode { get; set; }
        public long CompanyId { get; set; }
        public DateTime HolidayDate { get; set; }
        public string HolidayName { get; set; }
        public string HolidayType { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime? ModifedDate { get; set; }
        public long? ModifedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedUser { get; set; }
        public bool IsDelete { get; set; }
    }
}
