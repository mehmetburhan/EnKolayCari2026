using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;
using Microsoft.AspNetCore.Identity;

namespace EnKolayCari.Domain.Model.Dbo
{
    public class AspNetRoleClaims: IdentityRoleClaim<string>
    {
        public string RoleId { get; set; }
        public virtual AspNetRoles AspNetRoles { get; set; }
    }
}
