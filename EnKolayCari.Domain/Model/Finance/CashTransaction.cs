using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Finance
{
    public class CashTransaction  //Eski tablo: KasaHareket (TICARI_SLAVE1). Yeni şema: finance.CashTransaction
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: KasaHareket.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: KasaHareket.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: KasaHareket.SirketTanimId
        public long CashRegisterId { get; set; }  //Kasa (FK -> KasaTanim.Id). | Eski alan: KasaHareket.KasaTanimId
        public long AccountTransactionId { get; set; }  //Bagli cari hareket (FK -> CariHareket.Id). | Eski alan: KasaHareket.CariHareketId
        public DateTime TransactionDate { get; set; }  //Hareket tarihi. | Eski alan: KasaHareket.Tarih
        public int CashRegisterTransactionType { get; set; }  //Kasa hareket tipi. | Eski alan: KasaHareket.KasaHareketTipi
        public string Description { get; set; }  //Hareket aciklamasi. | Eski alan: KasaHareket.Aciklama
        public decimal DebitAmount { get; set; }  //Borc tutari. | Eski alan: KasaHareket.Borc
        public decimal CreditAmount { get; set; }  //Alacak tutari. | Eski alan: KasaHareket.Alacak
        public string CurrencyCode { get; set; }  //Para birimi kodu. | Eski alan: KasaHareket.DovizKodu
        public string Label { get; set; }  //Etiket. | Eski alan: KasaHareket.Etiket
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: KasaHareket.InsertDateTime
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: KasaHareket.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: KasaHareket.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: KasaHareket.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: KasaHareket.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: KasaHareket.DeleteUser
        public DateTime? RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: KasaHareket.RecDateTime

        public virtual Company Company { get; set; }
        public virtual CashRegister CashRegister { get; set; }
    }
}
