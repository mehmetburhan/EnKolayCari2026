using System;

namespace EnKolayCari2026.Application.Model.DTO.Inventory
{
    public class StorePersonalDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public long InsertUser { get; set; }
        public DateTime InsertDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public DateTime RecordDateTime { get; set; }
        public long StoreId { get; set; }
        public long PersonalId { get; set; }
    }
}
