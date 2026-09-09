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
    public class AspNetUserTokens : IdentityUserToken<string>
    {
        public DateTime? ExpireDate { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
