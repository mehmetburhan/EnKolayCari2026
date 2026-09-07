using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class CentralCurrency  //Eski tablo: MerkezDoviz (TICARI_MASTER). Yeni şema: common.CentralCurrency
    {
        public int SortOrder { get; set; }  //Listeleme sirasi. | Eski alan: MerkezDoviz.Sira
        public bool Stat { get; set; }  //Aktif mi? | Eski alan: MerkezDoviz.Aktif
        public string Code { get; set; }  //Doviz kodu (PK). | Eski alan: MerkezDoviz.Kod
        public string Info { get; set; }  //Doviz aciklamasi. | Eski alan: MerkezDoviz.Info
    }
}
