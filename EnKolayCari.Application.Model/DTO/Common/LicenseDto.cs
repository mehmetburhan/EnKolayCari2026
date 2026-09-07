using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class LicenseDto
    {
        public long Id { get; set; }
        public Guid? GId { get; set; }
        public long CompanyId { get; set; }
        public bool? Hediye { get; set; }
        public string LicenseType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? TahsilatSekli { get; set; }
        public decimal TahsilatTutari { get; set; }
        public DateTime InsertDateTime { get; set; }
        public long InsertUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public long? DeleteUser { get; set; }
    }
}
