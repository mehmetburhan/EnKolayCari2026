using System;

namespace EnKolayCari.Application.Model.DTO.Dbo
{
    public class AspNetUserTokensDto
    {
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
