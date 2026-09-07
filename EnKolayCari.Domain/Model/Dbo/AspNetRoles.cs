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
    public class AspNetRoles : IdentityRole
    {
        public virtual ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaims>();
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; } = new List<AspNetUserRoles>();
        public virtual ICollection<MenuRole> MenuRole { get; set; } = new List<MenuRole>();
    }
}
