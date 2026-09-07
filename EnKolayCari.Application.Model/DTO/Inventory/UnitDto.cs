using System;

namespace EnKolayCari.Application.Model.DTO.Inventory
{
    public class UnitDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Hassasiyet { get; set; }
        public string Label { get; set; }
        public DateTime? InsertDateTime { get; set; }
        public long? InsertUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? RecordDateTime { get; set; }
    }
}
