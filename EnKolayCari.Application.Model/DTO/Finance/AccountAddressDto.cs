using System;

namespace EnKolayCari.Application.Model.DTO.Finance
{
    public class AccountAddressDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public long AccountId { get; set; }
        public string AddressTitle { get; set; }
        public string AdSoyad { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long? InsertUser { get; set; }
        public DateTime? InsertDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime RecordDateTime { get; set; }
    }
}
