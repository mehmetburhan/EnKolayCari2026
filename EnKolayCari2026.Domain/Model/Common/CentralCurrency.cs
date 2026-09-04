using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class CentralCurrency  //Eski tablo: MerkezDoviz (TICARI_MASTER). Yeni şema: common.CentralCurrency
    {
        public int SortOrder { get; set; }  //Listeleme sirasi. | Eski alan: MerkezDoviz.Sira
        public bool Stat { get; set; }  //Aktif mi? | Eski alan: MerkezDoviz.Aktif
        public string Code { get; set; }  //Doviz kodu (PK). | Eski alan: MerkezDoviz.Kod
        public string Info { get; set; }  //Doviz aciklamasi. | Eski alan: MerkezDoviz.Info
    }
}
