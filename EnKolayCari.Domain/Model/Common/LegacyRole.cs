using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class LegacyRole  //Eski tablo: Roles (TICARI_MASTER). Yeni şema: common.LegacyRole
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: Roles.Id
        public Guid GId { get; set; }  //Rol GUID. | Eski alan: Roles.GId
        public string AppCode { get; set; }  //Uygulama kodu. | Eski alan: Roles.AppCode
        public string RoleCode { get; set; }  //Rol kodu. | Eski alan: Roles.RoleCode
        public string RoleName { get; set; }  //Rol adi. | Eski alan: Roles.RoleName
        public string GroupCode { get; set; }  //Menu/grup kodu. | Eski alan: Roles.GroupCode
        public string SubGroupCode { get; set; }  //Alt grup kodu. | Eski alan: Roles.SubGroupCode
        public int GroupOrdered { get; set; }  //Grup siralama. | Eski alan: Roles.GroupOrdered
        public int SubGroupOrdered { get; set; }  //Alt grup siralama. | Eski alan: Roles.SubGroupOrdered
    }
}
