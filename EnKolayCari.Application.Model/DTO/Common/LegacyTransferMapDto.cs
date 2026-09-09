using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class LegacyTransferMapDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public string LegacyTableName { get; set; }
        public Guid LegacyGId { get; set; }
        public DateTime? LegacyLastChangeDate { get; set; }
        public string NewTableName { get; set; }
        public Guid NewGId { get; set; }
        public DateTime InsertDateTime { get; set; }
    }
}
