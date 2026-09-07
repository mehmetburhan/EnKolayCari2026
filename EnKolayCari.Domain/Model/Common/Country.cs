using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class Country  //AI platform tablosu. Sirkete bagli ulke listesi.
    {
        public long Id { get; set; }  //Primary key.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public long CompanyId { get; set; }  //Sirket Id.
        public string CountryCode { get; set; }  //Ulke kodu.
        public bool Stat { get; set; }  //Aktif/pasif.
        public string Description { get; set; }  //Ulke adi.
        public int Ordered { get; set; }  //Siralama.
    }
}
