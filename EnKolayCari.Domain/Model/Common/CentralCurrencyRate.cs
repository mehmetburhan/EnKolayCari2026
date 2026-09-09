using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class CentralCurrencyRate  //Eski tablo: MerkezDovizKur (TICARI_MASTER). Yeni şema: common.CentralCurrencyRate
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: MerkezDovizKur.Id
        public DateTime TransactionDate { get; set; }  //Kur tarihi. | Eski alan: MerkezDovizKur.Tarih
        public string CurrencyCode { get; set; }  //Doviz kodu (FK -> MerkezDoviz.Kod). | Eski alan: MerkezDovizKur.DovizKodu
        public decimal Value { get; set; }  //Kur degeri. | Eski alan: MerkezDovizKur.Deger
        public string DefaultCurrencyCode { get; set; }  //Karsilastirma para birimi (genelde TRY). | Eski alan: MerkezDovizKur.StandartDovizKodu
        public DateTime? InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: MerkezDovizKur.InsertDateTime
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: MerkezDovizKur.UpdateDateTime
    }
}
