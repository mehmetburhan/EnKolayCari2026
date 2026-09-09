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
    public class AspNetUsers : IdentityUser
    {
        public string FullName { get; set; }
        public long CompanyId { get; set; }
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; } = new List<AspNetUserClaims>();
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; } = new List<AspNetUserLogins>();
        public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; } = new List<AspNetUserTokens>();
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; } = new List<AspNetUserRoles>();
    }
}
