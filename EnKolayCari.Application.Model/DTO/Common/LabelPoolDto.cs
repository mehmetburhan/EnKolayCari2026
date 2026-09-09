using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class LabelPoolDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public string TableName { get; set; }
        public long TableId { get; set; }
        public string Label { get; set; }
        public long? LabelDefId { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime? ModifedDate { get; set; }
        public long? ModifedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedUser { get; set; }
        public bool IsDelete { get; set; }
    }
}
