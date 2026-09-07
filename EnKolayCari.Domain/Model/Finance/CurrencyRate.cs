using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Finance
{
    public class CurrencyRate  //Eski tablo: DovizKur (TICARI_SLAVE1). Yeni şema: finance.CurrencyRate
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: DovizKur.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: DovizKur.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: DovizKur.SirketTanimId
        public DateTime TransactionDate { get; set; }  //Kur tarihi. | Eski alan: DovizKur.Tarih
        public string CurrencyCode { get; set; }  //Doviz kodu. | Eski alan: DovizKur.DovizKodu
        public decimal Value { get; set; }  //Kur degeri. | Eski alan: DovizKur.Deger
        public string DefaultCurrencyCode { get; set; }  //Karsilastirma para birimi (genelde TRY). | Eski alan: DovizKur.StandartDovizKodu
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: DovizKur.InsertUser
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: DovizKur.InsertDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: DovizKur.UpdateUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: DovizKur.UpdateDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: DovizKur.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: DovizKur.DeleteDateTime
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: DovizKur.RecDateTime

        public virtual Company Company { get; set; }
    }
}
