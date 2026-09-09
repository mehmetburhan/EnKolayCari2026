using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Finance
{
    public class CheckNote  //Eski tablo: CekSenetTanim (TICARI_SLAVE1). Yeni şema: finance.CheckNote
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: CekSenetTanim.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: CekSenetTanim.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: CekSenetTanim.SirketTanimId
        public long AccountId { get; set; }  //Cari sahibi (FK -> CariTanim.Id). | Eski alan: CekSenetTanim.CariTanimId
        public long KimdeAccountDefId { get; set; }  //Belgenin su an kimde oldugu cari. | Eski alan: CekSenetTanim.KimdeCariTanimId
        public string DocumentNo { get; set; }  //Cek/senet numarasi. | Eski alan: CekSenetTanim.BelgeNo
        public int DocumentStatus { get; set; }  //Portfoyde=100, BankadaTahsilde=200, BankadaTeminatta=300, MusteriyeVerildi=400, Cirolandi=500, Karsiliksiz=900, TahsilEdildi=1000 | Eski alan: CekSenetTanim.BelgeDurum
        public DateTime TransactionDate { get; set; }  //Duzenleme tarihi. | Eski alan: CekSenetTanim.Tarih
        public DateTime DueDate { get; set; }  //Vade tarihi. | Eski alan: CekSenetTanim.VadeTarihi
        public int NoteType { get; set; }  //VerilenFirmaCeki=202, VerilenMusteriCeki=203, VerilenFirmaSenet=204, VerilenMusteriSenet=205 | Eski alan: CekSenetTanim.HareketTipi
        public decimal Amount { get; set; }  //Tutar. | Eski alan: CekSenetTanim.Tutar
        public string CurrencyCode { get; set; }  //Para birimi. | Eski alan: CekSenetTanim.DovizKodu
        public string DocumentOriginalOwnerTitle { get; set; }  //Belgenin ilk sahibi unvani. | Eski alan: CekSenetTanim.BelgeIlkSahibiUnvan
        public string TCKN { get; set; }  //TC/VKN. | Eski alan: CekSenetTanim.TCKN
        public string OwningBank { get; set; }  //Ait oldugu banka. | Eski alan: CekSenetTanim.AitOlduguBanka
        public string BankBranch { get; set; }  //Banka subesi. | Eski alan: CekSenetTanim.BankaSubesi
        public string KesideYeri { get; set; }  //Keside yeri. | Eski alan: CekSenetTanim.KesideYeri
        public string IBAN { get; set; }  //IBAN. | Eski alan: CekSenetTanim.IBAN
        public string MersisNumber { get; set; }  //MERSIS no. | Eski alan: CekSenetTanim.MersisNo
        public string FindeksNo { get; set; }  //Findeks no. | Eski alan: CekSenetTanim.FindeksNo
        public string Label { get; set; }  //Etiket. | Eski alan: CekSenetTanim.Etiket
        public DateTime InsertDatetime { get; set; }  //Olusturma tarihi. | Eski alan: CekSenetTanim.InsertDatetime
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: CekSenetTanim.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: CekSenetTanim.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: CekSenetTanim.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: CekSenetTanim.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: CekSenetTanim.DeleteUser
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: CekSenetTanim.RecDateTime

        public virtual Company Company { get; set; }
        public virtual Account Account { get; set; }
    }
}
