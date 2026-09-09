using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Inventory
{
    public class StockCount  //Eski tablo: SayimTanim (TICARI_SLAVE1). Yeni şema: inventory.StockCount
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: SayimTanim.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: SayimTanim.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: SayimTanim.SirketTanimId
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: SayimTanim.InsertUser
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: SayimTanim.InsertDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: SayimTanim.UpdateUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: SayimTanim.UpdateDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: SayimTanim.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: SayimTanim.DeleteDateTime
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: SayimTanim.RecDateTime
        public DateTime StockCountDate { get; set; }  //Sayim tarihi. | Eski alan: SayimTanim.SayimTarihi
        public string StockCountDescription { get; set; }  //Sayim aciklamasi. | Eski alan: SayimTanim.SayimAciklama
        public int StockCountStatus { get; set; }  //- | Eski alan: SayimTanim.SayimDurumu
        public long StoreId { get; set; }  //Sayim yapilan depo (FK -> DepoTanim.Id). | Eski alan: SayimTanim.DepoTanimId
        public long InboundTradeDocumentId { get; set; }  //Sayim farki giris faturasi (FK -> Fatura.Id). | Eski alan: SayimTanim.GirisFaturaId
        public long OutboundTradeDocumentId { get; set; }  //Sayim farki cikis faturasi (FK -> Fatura.Id). | Eski alan: SayimTanim.CikisFaturaId
    }
}
