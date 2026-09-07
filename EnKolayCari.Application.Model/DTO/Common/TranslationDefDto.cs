using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class TranslationDefDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public string TableName { get; set; }
        public long TableId { get; set; }
        public string TableFieldName { get; set; }
        public string LanguageCode { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
        public DateTime? ModifedDate { get; set; }
        public long? ModifedUser { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedUser { get; set; }
        public long IsDelete { get; set; }
    }
}
