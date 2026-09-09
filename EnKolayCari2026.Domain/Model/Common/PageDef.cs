using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class PageDef  //Eski tablo: SayfaTanim (TICARI_MASTER). Yeni şema: common.PageDef
    {
        public long Id { get; set; }  //Sayfa ID. | Eski alan: SayfaTanim.Id
        public string Description { get; set; }  //Sayfa aciklamasi. | Eski alan: SayfaTanim.Aciklama
        public string Url { get; set; }  //Sayfa URL yolu. | Eski alan: SayfaTanim.Url
        public string LicenseType { get; set; }  //Gerekli minimum lisans tipi. | Eski alan: SayfaTanim.LisansTipi
    }
}
