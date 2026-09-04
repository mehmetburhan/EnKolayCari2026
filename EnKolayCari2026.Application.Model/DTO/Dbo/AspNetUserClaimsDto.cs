using System;

namespace EnKolayCari2026.Application.Model.DTO.Dbo
{
    public class AspNetUserClaimsDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
