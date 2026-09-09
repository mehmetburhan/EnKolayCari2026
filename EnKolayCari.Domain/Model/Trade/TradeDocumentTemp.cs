using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;

namespace EnKolayCari.Domain.Model.Trade
{
    public class TradeDocumentTemp  //Eski tablo: FaturaTemp (TICARI_SLAVE1). Yeni şema: trade.TradeDocumentTemp
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: FaturaTemp.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: FaturaTemp.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: FaturaTemp.SirketTanimId
        public Guid PersonalGId { get; set; }  //Personel/kullanici GUID. | Eski alan: FaturaTemp.PersonelGId
        public int DocumentType { get; set; }  //Belge hareket tipi. | Eski alan: FaturaTemp.HareketTipi
        public long? AccountId { get; set; }  //Bagli cari (FK -> CariTanim.Id). | Eski alan: FaturaTemp.CariTanimId
        public string TradeDocumentNo { get; set; }  //Belge/fatura numarasi. | Eski alan: FaturaTemp.FaturaNo
        public DateTime? TransactionDate { get; set; }  //Belge tarihi. | Eski alan: FaturaTemp.Tarih
        public string CurrencyCode { get; set; }  //Para birimi kodu. | Eski alan: FaturaTemp.DovizKodu
        public string Address { get; set; }  //Adres. | Eski alan: FaturaTemp.Adres

        public virtual Company Company { get; set; }
        public virtual Account Account { get; set; }
    }
}
