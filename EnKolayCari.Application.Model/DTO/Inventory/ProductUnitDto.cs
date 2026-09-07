using System;

namespace EnKolayCari.Application.Model.DTO.Inventory
{
    public class ProductUnitDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public bool Stat { get; set; }
        public long ProductId { get; set; }
        public long UnitId { get; set; }
        public string Barcode { get; set; }
        public double Carpan { get; set; }
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
