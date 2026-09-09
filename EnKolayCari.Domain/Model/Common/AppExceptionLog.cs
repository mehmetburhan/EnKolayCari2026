using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class AppExceptionLog  //AI platform tablosu. Uygulama / istemci hata loglari (WebAPI, WebUI, Mobil).
    {
        public long Id { get; set; }  //Primary key.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public long? CompanyId { get; set; }  //Sirket FK (opsiyonel).
        public long? PersonalId { get; set; }  //Ilgili personel FK (opsiyonel).
        public string DeviceId { get; set; }  //Cihaz kimligi (CustomHeader).
        public string ClientOs { get; set; }  //Istemci isletim sistemi.
        public string AppVersion { get; set; }  //Uygulama surumu.
        public string Language { get; set; }  //Istemci dil kodu (or. tr-TR).
        public string IpAddress { get; set; }  //Istemci IP.
        public string ExceptionType { get; set; }  //Exception tip adi.
        public string ExceptionMessage { get; set; }  //Hata mesaji.
        public string StackTrace { get; set; }  //Stack trace.
        public string Source { get; set; }  //Kaynak (assembly / modul).
        public string PageOrScreen { get; set; }  //Sayfa veya ekran adi.
        public string ActionOrMethod { get; set; }  //Action / method adi.
        public string RequestUrl { get; set; }  //HTTP istek URL.
        public string RequestMethod { get; set; }  //HTTP method (GET/POST/...).
        public string SeverityLevel { get; set; }  //Siddet (Error/Warning/Info...).
        public string AdditionalData { get; set; }  //Ek JSON / serbest veri.
        public DateTime OccurredAt { get; set; }  //Hatanin olustugu zaman.
        public bool Stat { get; set; }  //Aktif/pasif.
        public DateTime CreatedDate { get; set; }  //Kayit olusturma.
        public long CreatedUser { get; set; }  //Olusturan kullanici Id.
        public DateTime? ModifedDate { get; set; }  //Guncelleme tarihi.
        public long? ModifedUser { get; set; }  //Guncelleyen kullanici Id.
        public DateTime? DeletedDate { get; set; }  //Soft-delete tarihi.
        public long? DeletedUser { get; set; }  //Soft-delete yapan kullanici Id.
        public bool IsDelete { get; set; }  //Soft-delete bayragi (bit).

        public virtual Company Company { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
