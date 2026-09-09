using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;
using Microsoft.AspNetCore.Identity;

namespace EnKolayCari2026.Domain.Model.Dbo
{
    public class AspNetUserRoles : IdentityUserRole<string>
    {
        public long CompanyId { get; set; }  // ✅ NOT NULL - PK'nın bir parçası
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual AspNetRoles AspNetRoles { get; set; }
        public virtual Company Company { get; set; }
    }
}
