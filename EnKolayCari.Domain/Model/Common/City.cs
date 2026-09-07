using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class City  //Sehir/ilce hiyerarsisi (AI mimari). Level 1=Il, Level 2=Ilce. Eski: Iller + Ilceler.
    {
        public long Id { get; set; }  //Birincil anahtar (Identity)
        public Guid GId { get; set; }  //Kuresel benzersiz kimlik (Guid)
        public bool Stat { get; set; }  //Aktif/pasif (1=aktif)
        public string FullCityCode { get; set; }  //Bilesik sehir kodu (ulke + il/ilce). Unique anahtar.
        public string CountryCode { get; set; }  //Ulke kodu (ornek: TR)
        public string CityCode { get; set; }  //Il veya ilce kodu
        public string ParentCityCode { get; set; }  //Ust il CityCode (Level=2 iken). Level=1 icin null.
        public string CityName { get; set; }  //Il veya ilce adi. Eski: Iller.IlAdi / Ilceler.IlceAdi
        public int Level { get; set; }  //Seviye: 1=Il, 2=Ilce. Eski Iller+Ilceler hiyerarsisi.
    }
}
