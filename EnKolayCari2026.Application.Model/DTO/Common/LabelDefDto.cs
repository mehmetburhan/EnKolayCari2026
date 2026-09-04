using System;

namespace EnKolayCari2026.Application.Model.DTO.Common
{
    public class LabelDefDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public bool Stat { get; set; }
        public string Label { get; set; }
        public string LabelDescription { get; set; }
        public string SeoKey { get; set; }
        public bool? IsMainCategory { get; set; }
        public int? Order { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime? ModifedDate { get; set; }
        public long? ModifedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedUser { get; set; }
        public bool IsDelete { get; set; }
    }
}
