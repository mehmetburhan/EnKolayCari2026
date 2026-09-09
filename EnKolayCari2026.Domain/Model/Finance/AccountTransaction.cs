using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Finance
{
    public class AccountTransaction  //Eski tablo: CariHareket (TICARI_SLAVE1). Yeni şema: finance.AccountTransaction
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: CariHareket.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: CariHareket.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: CariHareket.SirketTanimId
        public long AccountId { get; set; }  //Bagli cari (FK -> CariTanim.Id). | Eski alan: CariHareket.CariTanimId
        public DateTime TransactionDate { get; set; }  //Hareket tarihi. | Eski alan: CariHareket.Tarih
        public DateTime? DueDate { get; set; }  //Vade tarihi. | Eski alan: CariHareket.VadeTarihi
        public string DocumentNo { get; set; }  //Belge/evrak numarasi. | Eski alan: CariHareket.BelgeNo
        public int HareketType { get; set; }  //SatisFaturasi=100, AlisFaturasi=200, NakitTahsilat=1, NakitOdeme=2, AlinanCek=102, AlinanSenet=103, BankaDekontTahsilat=11, BankaDekontOdeme=21, VerilenFirmaCeki=202, VerilenMusteriCeki=203, VerilenFirmaSenet=204, VerilenMusteriSenet=205 | Eski alan: CariHareket.HareketTipi
        public long? TradeDocumentId { get; set; }  //Iliskili fatura (FK -> Fatura.Id). | Eski alan: CariHareket.FaturaId
        public string Description { get; set; }  //Hareket aciklamasi. | Eski alan: CariHareket.Aciklama
        public decimal DebitAmount { get; set; }  //Borc tutari. | Eski alan: CariHareket.Borc
        public decimal CreditAmount { get; set; }  //Alacak tutari. | Eski alan: CariHareket.Alacak
        public string CurrencyCode { get; set; }  //Para birimi kodu. | Eski alan: CariHareket.DovizKodu
        public long? RelatedAccountTransactionId { get; set; }  //Bagli/karsilik cari hareket ID. | Eski alan: CariHareket.BagliCariHareketId
        public long? CheckNoteDefId { get; set; }  //Iliskili cek/senet (FK). | Eski alan: CariHareket.CekSenetTanimId
        public long? CashRegisterId { get; set; }  //Iliskili kasa (FK -> KasaTanim.Id). | Eski alan: CariHareket.KasaTanimId
        public string Label { get; set; }  //Etiket. | Eski alan: CariHareket.Etiket
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: CariHareket.InsertDateTime
        public long? InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: CariHareket.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: CariHareket.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: CariHareket.UpdateUser
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: CariHareket.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: CariHareket.DeleteDateTime
        public DateTime? RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: CariHareket.RecDateTime

        public virtual Company Company { get; set; }
        public virtual Account Account { get; set; }
        public virtual TradeDocument TradeDocument { get; set; }
        public virtual CashRegister CashRegister { get; set; }
    }
}
