using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Inventory
{
    public class StockCountLine  //Eski tablo: SayimHareket (TICARI_SLAVE1). Yeni şema: inventory.StockCountLine
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: SayimHareket.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: SayimHareket.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: SayimHareket.SirketTanimId
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: SayimHareket.InsertUser
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: SayimHareket.InsertDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: SayimHareket.UpdateUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: SayimHareket.UpdateDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: SayimHareket.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: SayimHareket.DeleteDateTime
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: SayimHareket.RecDateTime
        public long StockCountId { get; set; }  //Sayim (FK -> SayimTanim.Id). | Eski alan: SayimHareket.SayimId
        public long ProductId { get; set; }  //Urun (FK -> UrunTanim.Id). | Eski alan: SayimHareket.UrunTanimId
        public string Color { get; set; }  //Renk bilgisi. | Eski alan: SayimHareket.Renk
        public string Barcode { get; set; }  //Barkod. | Eski alan: SayimHareket.Barkod
        public decimal Quantity { get; set; }  //Sayilan miktar. | Eski alan: SayimHareket.Miktar
        public long TradeDocumentLineId { get; set; }  //Iliskili fatura satiri (FK -> FaturaHareket.Id). | Eski alan: SayimHareket.FaturaHareketId
    }
}
