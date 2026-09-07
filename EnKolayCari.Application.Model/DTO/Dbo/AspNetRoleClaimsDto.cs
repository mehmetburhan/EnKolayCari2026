using System;

namespace EnKolayCari.Application.Model.DTO.Dbo
{
    public class AspNetRoleClaimsDto
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
