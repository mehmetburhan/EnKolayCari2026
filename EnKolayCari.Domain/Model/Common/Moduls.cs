using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class Moduls  //Modul katalogu (AI mimari). Eski tablo: Modul (TICARI_MASTER). Id IDENTITY degildir.
    {
        public long Id { get; set; }  //Modul Id (manuel seed). Eski alan: Modul.Id
        public string ModulName { get; set; }  //Modul adi. Eski alan: Modul (ad/aciklama alanlari)
        public virtual ICollection<ProcessFlow> ProcessFlow { get; set; } = new List<ProcessFlow>();
    }
}
