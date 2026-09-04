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
    public class AspNetUserClaims : IdentityUserClaim<string>
    {
        public string UserId { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
