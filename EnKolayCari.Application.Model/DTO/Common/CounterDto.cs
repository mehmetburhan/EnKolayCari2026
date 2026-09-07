using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class CounterDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public string Type { get; set; }
        public string Seri { get; set; }
        public int BaslangicNo { get; set; }
        public int BitisNo { get; set; }
        public int NextNumber { get; set; }
        public long InsertUser { get; set; }
        public DateTime InsertDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public DateTime RecordDateTime { get; set; }
    }
}
