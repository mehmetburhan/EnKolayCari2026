using System;

namespace EnKolayCari2026.Application.Model.DTO.Dbo
{
    public class AspNetUserLoginsDto
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public string UserId { get; set; }
    }
}
