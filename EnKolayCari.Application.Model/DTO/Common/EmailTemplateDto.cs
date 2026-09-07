using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class EmailTemplateDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public bool Stat { get; set; }
        public string Konu { get; set; }
        public string Metin { get; set; }
        public DateTime InsertDateTime { get; set; }
        public long InsertUser { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public long? UpdateUser { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public long? DeleteUser { get; set; }
        public DateTime RecordDateTime { get; set; }
    }
}
