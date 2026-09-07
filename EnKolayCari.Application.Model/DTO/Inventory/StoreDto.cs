using System;

namespace EnKolayCari.Application.Model.DTO.Inventory
{
    public class StoreDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public bool Stat { get; set; }
        public bool IsEcommerceSaleActive { get; set; }
        public bool IsMainStore { get; set; }
        public string StoreCode { get; set; }
        public string Description { get; set; }
        public string AuthorizedPerson { get; set; }
        public string Label { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public DateTime? InsertDateTime { get; set; }
        public long? InsertUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? RecordDateTime { get; set; }
    }
}
