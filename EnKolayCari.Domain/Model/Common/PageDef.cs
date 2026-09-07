using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class PageDef  //Eski tablo: SayfaTanim (TICARI_MASTER). Yeni şema: common.PageDef
    {
        public long Id { get; set; }  //Sayfa ID. | Eski alan: SayfaTanim.Id
        public string Description { get; set; }  //Sayfa aciklamasi. | Eski alan: SayfaTanim.Aciklama
        public string Url { get; set; }  //Sayfa URL yolu. | Eski alan: SayfaTanim.Url
        public string LicenseType { get; set; }  //Gerekli minimum lisans tipi. | Eski alan: SayfaTanim.LisansTipi
    }
}
