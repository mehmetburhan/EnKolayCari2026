using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Dbo
{
    public class MenuRole  //Sirket kapsamli menu-rol eslemesi. Eski: SayfaTanim + Roles.
    {
        public long Id { get; set; } 
        public long CompanyId { get; set; } 
        public string MenuKey { get; set; } 
        public string RoleId { get; set; } 
        public DateTime CreatedDate { get; set; } 
        public long CreatedUser { get; set; } 
    }
}
