using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class Counter  //Eski tablo: Sayac (TICARI_SLAVE1). Yeni şema: common.Counter
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: Sayac.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: Sayac.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: Sayac.SirketTanimId
        public string Type { get; set; }  //Sayac tipi (Fatura, Irsaliye vb.). | Eski alan: Sayac.Tip
        public string SerialCode { get; set; }  //Belge seri kodu. | Eski alan: Sayac.Seri
        public int StartNumber { get; set; }  //Baslangic numarasi. | Eski alan: Sayac.BaslangicNo
        public int EndNumber { get; set; }  //Bitis numarasi. | Eski alan: Sayac.BitisNo
        public int NextNumber { get; set; }  //Siradaki numara. | Eski alan: Sayac.SiradakiNo
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: Sayac.InsertUser
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: Sayac.InsertDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: Sayac.UpdateUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: Sayac.UpdateDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: Sayac.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: Sayac.DeleteDateTime
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: Sayac.RecDateTime

        public virtual Company Company { get; set; }
    }
}
