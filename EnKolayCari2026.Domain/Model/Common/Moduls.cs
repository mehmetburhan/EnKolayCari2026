using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class Moduls  //Modul katalogu (AI mimari). Eski tablo: Modul (TICARI_MASTER). Id IDENTITY degildir.
    {
        public long Id { get; set; }  //Modul Id (manuel seed). Eski alan: Modul.Id
        public string ModulName { get; set; }  //Modul adi. Eski alan: Modul (ad/aciklama alanlari)
        public virtual ICollection<ProcessFlow> ProcessFlow { get; set; } = new List<ProcessFlow>();
    }
}
