using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;

namespace EnKolayCari.Domain.Model.Trade
{
    public class TradeDocumentLineTemp  //Eski tablo: FaturaHareketTemp (TICARI_SLAVE1). Yeni şema: trade.TradeDocumentLineTemp
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: FaturaHareketTemp.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: FaturaHareketTemp.GId
        public Guid PersonalGId { get; set; }  //Personel/kullanici GUID (sepet sahibi). | Eski alan: FaturaHareketTemp.PersonelGId
        public long? CartId { get; set; }  //Sepet oturum ID. | Eski alan: FaturaHareketTemp.SepetId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: FaturaHareketTemp.SirketTanimId
        public int DocumentType { get; set; }  //Belge hareket tipi. | Eski alan: FaturaHareketTemp.HareketTipi
        public Guid ProductGId { get; set; }  //Urun GUID. | Eski alan: FaturaHareketTemp.UrunGId
        public string ProductDescription { get; set; }  //Satir urun aciklamasi. | Eski alan: FaturaHareketTemp.UrunAciklama
        public string SerialNo { get; set; }  //Seri numarasi. | Eski alan: FaturaHareketTemp.SeriNo
        public string ColorSize { get; set; }  //Renk/beden bilgisi. | Eski alan: FaturaHareketTemp.RenkBeden
        public decimal Quantity { get; set; }  //Miktar. | Eski alan: FaturaHareketTemp.Miktar
        public long UnitId { get; set; }  //Birim (FK -> BirimTanim.Id). | Eski alan: FaturaHareketTemp.BirimId
        public decimal? UnitMultiplier { get; set; }  //Birim donusum katsayisi. | Eski alan: FaturaHareketTemp.BirimKatsayi
        public decimal UnitPrice { get; set; }  //Birim fiyati (KDV haric). | Eski alan: FaturaHareketTemp.BirimFiyati
        public decimal UnitPriceVatIncluded { get; set; }  //Birim fiyati (KDV dahil). | Eski alan: FaturaHareketTemp.BirimFiyatiKdvDahil
        public string CurrencyCode { get; set; }  //Para birimi kodu. | Eski alan: FaturaHareketTemp.DovizKodu
        public int VatRate { get; set; }  //KDV orani (%). | Eski alan: FaturaHareketTemp.KdvOrani
        public string VatDH { get; set; }  //KDV durumu: D=Dahil, H=Haric. | Eski alan: FaturaHareketTemp.KdvDH
        public decimal? TotalAmountExVat { get; set; }  //Toplam tutar (KDV haric). | Eski alan: FaturaHareketTemp.ToplamTutarKdvHaric
        public decimal TotalAmount { get; set; }  //Toplam tutar (KDV dahil). | Eski alan: FaturaHareketTemp.ToplamTutar
        public Guid? TradeDocumentGId { get; set; }  //Bagli fatura GUID. | Eski alan: FaturaHareketTemp.FaturaGId
        public Guid? TradeDocumentTypeGId { get; set; }  //Bagli fatura satir GUID. | Eski alan: FaturaHareketTemp.FaturaHareketGId
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: FaturaHareketTemp.InsertDateTime
        public string EcommercePaymentId { get; set; }  //E-ticaret odeme referans ID. | Eski alan: FaturaHareketTemp.ETicaretOdemeId
        public bool IsSoldOut { get; set; }  //Stok tukendi mi? | Eski alan: FaturaHareketTemp.Tukenmis
        public Guid? StoreGId { get; set; }  //Depo GUID. | Eski alan: FaturaHareketTemp.DepoTanimGId
        public decimal? DiscountPercent { get; set; }  //Iskonto yuzdesi. | Eski alan: FaturaHareketTemp.IskontoYuzde
        public decimal? Discount1Percent { get; set; }  //Ikinci iskonto yuzdesi. | Eski alan: FaturaHareketTemp.Iskonto1Yuzde
        public decimal? DeferralPercent { get; set; }  //Vade farki yuzdesi. | Eski alan: FaturaHareketTemp.VadeFarkiYuzde

        public virtual Company Company { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
