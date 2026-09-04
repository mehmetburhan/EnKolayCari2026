using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;

namespace EnKolayCari2026.Domain.Model.Trade
{
    public class TradeDocumentLine  //Eski tablo: FaturaHareket (TICARI_SLAVE1). Yeni şema: trade.TradeDocumentLine
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: FaturaHareket.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: FaturaHareket.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: FaturaHareket.SirketTanimId
        public long TradeDocumentId { get; set; }  //Bagli belge (FK -> Fatura.Id). | Eski alan: FaturaHareket.FaturaId
        public long StoreId { get; set; }  //Islem yapilan depo (FK -> DepoTanim.Id). | Eski alan: FaturaHareket.DepoTanimId
        public int HareketType { get; set; }  //Tanimsiz = 0, | Eski alan: FaturaHareket.HareketTipi
        public long ProductId { get; set; }  //Bagli urun (FK -> UrunTanim.Id). | Eski alan: FaturaHareket.UrunTanimId
        public string ProductDescription { get; set; }  //Satir urun aciklamasi. | Eski alan: FaturaHareket.UrunAciklama
        public string SeriNo { get; set; }  //Seri numarasi. | Eski alan: FaturaHareket.SeriNo
        public string ColorSize { get; set; }  //Renk/beden bilgisi. | Eski alan: FaturaHareket.RenkBeden
        public decimal Quantity { get; set; }  //Miktar. | Eski alan: FaturaHareket.Miktar
        public long UnitId { get; set; }  //Birim (FK -> BirimTanim.Id). | Eski alan: FaturaHareket.BirimId
        public decimal? UnitKatsayi { get; set; }  //Birim donusum katsayisi. | Eski alan: FaturaHareket.BirimKatsayi
        public decimal UnitPrice { get; set; }  //Birim fiyati. | Eski alan: FaturaHareket.BirimFiyati
        public string CurrencyCode { get; set; }  //Para birimi kodu. | Eski alan: FaturaHareket.DovizKodu
        public int VatRate { get; set; }  //KDV orani (%). | Eski alan: FaturaHareket.KdvOrani
        public decimal VatTutari { get; set; }  //KDV tutari. | Eski alan: FaturaHareket.KdvTutari
        public decimal SatirTutari { get; set; }  //Satir tutari (KDV haric). | Eski alan: FaturaHareket.SatirTutari
        public bool? StokSayimiDahilEtme { get; set; }  //rsaliye faturaya dnnce irsaliye iinde kalan kalemler stok saymna dahil edilmesin diye bu yaplmtr. | Eski alan: FaturaHareket.StokSayimiDahilEtme
        public string Label { get; set; }  //Etiket. | Eski alan: FaturaHareket.Etiket
        public DateTime? InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: FaturaHareket.InsertDateTime
        public long? InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: FaturaHareket.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: FaturaHareket.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: FaturaHareket.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: FaturaHareket.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: FaturaHareket.DeleteUser
        public DateTime? RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: FaturaHareket.RecDateTime
        public decimal? DiscountPercent { get; set; }  //Iskonto yuzdesi. | Eski alan: FaturaHareket.IskontoYuzde
        public decimal? Discount1Percent { get; set; }  //Ikinci iskonto yuzdesi. | Eski alan: FaturaHareket.Iskonto1Yuzde
        public decimal? VadeFarkiPercent { get; set; }  //Vade farki yuzdesi. | Eski alan: FaturaHareket.VadeFarkiYuzde

        public virtual Company Company { get; set; }
        public virtual TradeDocument TradeDocument { get; set; }
        public virtual Store Store { get; set; }
        public virtual Product Product { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
