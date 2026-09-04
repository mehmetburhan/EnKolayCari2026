using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Finance
{
    public class CheckNoteTransaction  //Eski tablo: CekSenetHareket (TICARI_SLAVE1). Yeni şema: finance.CheckNoteTransaction
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: CekSenetHareket.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: CekSenetHareket.GId
        public DateTime HareketDate { get; set; }  //Hareket tarihi. | Eski alan: CekSenetHareket.HareketTarihi
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: CekSenetHareket.SirketTanimId
        public long CheckNoteId { get; set; }  //Bagli cek/senet (FK). | Eski alan: CekSenetHareket.CekSenetId
        public int DocumentStatus { get; set; }  //Yeni belge durumu. | Eski alan: CekSenetHareket.BelgeDurum
        public long AccountId { get; set; }  //Iliskili cari. | Eski alan: CekSenetHareket.CariTanimId
        public long AccountHareketId { get; set; }  //Iliskili cari hareket. | Eski alan: CekSenetHareket.CariHareketId
        public long CashRegisterHareketId { get; set; }  //Iliskili kasa hareket. | Eski alan: CekSenetHareket.KasaHareketId
        public string Label { get; set; }  //Etiket. | Eski alan: CekSenetHareket.Etiket
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: CekSenetHareket.InsertDateTime
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: CekSenetHareket.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: CekSenetHareket.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: CekSenetHareket.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: CekSenetHareket.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: CekSenetHareket.DeleteUser
        public DateTime? RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: CekSenetHareket.RecDateTime

        public virtual Company Company { get; set; }
        public virtual Account Account { get; set; }
    }
}
