using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;

namespace EnKolayCari2026.Domain.Model.Trade
{
    public class TradeDocumentCurrency  //Eski tablo: FaturaDoviz (TICARI_SLAVE1). Yeni şema: trade.TradeDocumentCurrency
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: FaturaDoviz.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: FaturaDoviz.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: FaturaDoviz.SirketTanimId
        public long? TradeDocumentId { get; set; }  //Bagli fatura (FK -> Fatura.Id). | Eski alan: FaturaDoviz.FaturaId
        public string CurrencyCode { get; set; }  //Doviz kodu. | Eski alan: FaturaDoviz.DovizKodu
        public decimal? Value { get; set; }  //Kur degeri. | Eski alan: FaturaDoviz.Deger
        public DateTime? InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: FaturaDoviz.InsertDateTime
        public long? InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: FaturaDoviz.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: FaturaDoviz.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: FaturaDoviz.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: FaturaDoviz.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: FaturaDoviz.DeleteUser
        public DateTime? RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: FaturaDoviz.RecDateTime

        public virtual Company Company { get; set; }
        public virtual TradeDocument TradeDocument { get; set; }
    }
}
