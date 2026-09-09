using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Finance
{
    public class Currency  //Eski tablo: DovizTanim (TICARI_SLAVE1). Yeni şema: finance.Currency
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: DovizTanim.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: DovizTanim.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: DovizTanim.SirketTanimId
        public int SortOrder { get; set; }  //Listeleme sirasi. | Eski alan: DovizTanim.Sira
        public string CurrencyCode { get; set; }  //Doviz kodu (USD, EUR vb.). | Eski alan: DovizTanim.DovizKodu
        public int DecimalPrecision { get; set; }  //Ondalik hassasiyet. | Eski alan: DovizTanim.Hassasiyet
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: DovizTanim.InsertUser
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: DovizTanim.InsertDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: DovizTanim.UpdateUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: DovizTanim.UpdateDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: DovizTanim.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: DovizTanim.DeleteDateTime
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: DovizTanim.RecDateTime

        public virtual Company Company { get; set; }
    }
}
