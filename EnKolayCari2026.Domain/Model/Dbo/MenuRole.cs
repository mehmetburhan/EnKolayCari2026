using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Dbo
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
