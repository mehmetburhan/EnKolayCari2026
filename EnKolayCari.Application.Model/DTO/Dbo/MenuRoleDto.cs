using System;

namespace EnKolayCari.Application.Model.DTO.Dbo
{
    public class MenuRoleDto
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string MenuKey { get; set; }
        public string RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUser { get; set; }
    }
}
