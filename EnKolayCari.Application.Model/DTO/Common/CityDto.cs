using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class CityDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public bool Stat { get; set; }
        public string FullCityCode { get; set; }
        public string CountryCode { get; set; }
        public string CityCode { get; set; }
        public string ParentCityCode { get; set; }
        public string CityName { get; set; }
        public int Level { get; set; }
    }
}
