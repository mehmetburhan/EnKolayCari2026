using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class ProcessType  //AI platform tablosu. Durum kodlari (ModulId + Code). Yeni durum = satir seed; tablo yapisi degismez.
    {
        public long Id { get; set; }  //Primary key (IDENTITY seed 0).
        public long ModulId { get; set; }  //FK mantigi → Moduls.Id.
        public string Code { get; set; }  //Durum kodu (PLANNED, COMPLETED...).
        public string DescriptionTR { get; set; }  //Turkce aciklama.
        public string DescriptionEN { get; set; }  //Ingilizce aciklama.
        public int Ordered { get; set; }  //Siralama.
        public string Color { get; set; }  //UI renk JSON/hex.
        public virtual ICollection<ProcessTypePersonal> ProcessTypePersonal { get; set; } = new List<ProcessTypePersonal>();
    }
}
