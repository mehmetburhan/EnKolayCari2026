using System;

namespace EnKolayCari.Application.Model.DTO.Common
{
    public class TokenDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public long CompanyId { get; set; }
        public long PersonalId { get; set; }
        public DateTime ExpreDate { get; set; }
        public string Application { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
