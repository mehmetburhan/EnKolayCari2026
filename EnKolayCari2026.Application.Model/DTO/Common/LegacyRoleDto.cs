using System;

namespace EnKolayCari2026.Application.Model.DTO.Common
{
    public class LegacyRoleDto
    {
        public long Id { get; set; }
        public Guid GId { get; set; }
        public string AppCode { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string GroupCode { get; set; }
        public string SubGroupCode { get; set; }
        public int GroupOrdered { get; set; }
        public int SubGroupOrdered { get; set; }
    }
}
