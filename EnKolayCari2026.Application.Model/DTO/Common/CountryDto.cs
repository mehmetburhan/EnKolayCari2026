using System;

namespace EnKolayCari2026.Application.Model.DTO.Common
{
    public class CountryDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public string CountryCode { get; set; }
        public bool Stat { get; set; }
        public string Description { get; set; }
        public int Ordered { get; set; }
    }
}
