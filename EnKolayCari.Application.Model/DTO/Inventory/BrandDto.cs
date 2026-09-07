using System;

namespace EnKolayCari.Application.Model.DTO.Inventory
{
    public class BrandDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public bool Stat { get; set; }
        public string BrandCode { get; set; }
        public string Description { get; set; }
        public long? TrendyolId { get; set; }
        public string TrendyolDescription { get; set; }
        public long InsertUser { get; set; }
        public DateTime InsertDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public DateTime RecordDateTime { get; set; }
    }
}
