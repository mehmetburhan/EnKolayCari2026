using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class CountryHolidays  //AI platform tablosu. Ulke / uygulama tatil takvimi.
    {
        public long Id { get; set; }  //Primary key.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public bool Stat { get; set; }  //Aktif/pasif.
        public string CountryCode { get; set; }  //Ulke kodu.
        public string AppCode { get; set; }  //Uygulama kodu.
        public long CompanyId { get; set; }  //Sirket Id.
        public DateTime HolidayDate { get; set; }  //Tatil tarihi.
        public string HolidayName { get; set; }  //Tatil adi.
        public string HolidayType { get; set; }  //Tatil tipi (NATIONAL vb.).
        public DateTime CreatedDate { get; set; }  //Olusturma tarihi.
        public long CreatedUser { get; set; }  //Olusturan kullanici Id.
        public DateTime? ModifedDate { get; set; }  //Guncelleme tarihi.
        public long? ModifedUser { get; set; }  //Guncelleyen kullanici Id.
        public DateTime? DeletedDate { get; set; }  //Soft-delete tarihi.
        public long? DeletedUser { get; set; }  //Soft-delete yapan kullanici Id.
        public bool IsDelete { get; set; }  //Soft-delete bayragi (bit).
    }
}
