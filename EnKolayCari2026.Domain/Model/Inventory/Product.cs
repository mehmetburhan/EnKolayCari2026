using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Inventory
{
    public class Product  //Eski tablo: UrunTanim (TICARI_SLAVE1). Yeni şema: inventory.Product
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: UrunTanim.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: UrunTanim.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: UrunTanim.SirketTanimId
        public bool Stat { get; set; }  //Urun aktif mi? | Eski alan: UrunTanim.Aktif
        public string ProductCode { get; set; }  //Urun kodu. | Eski alan: UrunTanim.UrunKodu
        public string OzelKod { get; set; }  //Ozel kod. | Eski alan: UrunTanim.OzelKod
        public long BrandId { get; set; }  //Marka (FK -> MarkaTanim.Id). | Eski alan: UrunTanim.MarkaTanimId
        public string ProductDescription { get; set; }  //Urun aciklamasi. | Eski alan: UrunTanim.UrunAciklama
        public string ProductKisaBilgi { get; set; }  //Kisa bilgi/ozet. | Eski alan: UrunTanim.UrunKisaBilgi
        public string Barcode { get; set; }  //Ana barkod. | Eski alan: UrunTanim.Barkod
        public long? ColorId { get; set; }  //Renk (FK -> UrunRenkPaleti.Id). | Eski alan: UrunTanim.RenkId
        public bool IsWeightBarcode { get; set; }  //Agirlik barkodu mu? | Eski alan: UrunTanim.KgBarkod
        public long UnitId { get; set; }  //Ana birim (FK -> BirimTanim.Id). | Eski alan: UrunTanim.BirimId
        public long? CategoryId { get; set; }  //Kategori/beden (FK -> KategoriTanim.Id). | Eski alan: UrunTanim.KategoriTanimId
        public decimal? CriticalStockQuantity { get; set; }  //Kritik stok esik miktari. | Eski alan: UrunTanim.KritikStokMiktari
        public decimal? InternetCriticalStockQuantity { get; set; }  //E-ticaret kritik stok esigi. | Eski alan: UrunTanim.InternetKritikStokMiktari
        public bool SeriNoTakip { get; set; }  //Seri numarasi takibi yapilsin mi? | Eski alan: UrunTanim.SeriNoTakip
        public int PurchaseVatRate { get; set; }  //Alis KDV orani (%). | Eski alan: UrunTanim.AlisKdvOrani
        public int VatRate { get; set; }  //Satis KDV orani (%). | Eski alan: UrunTanim.KdvOrani
        public decimal PurchasePrice { get; set; }  //Alis fiyati. | Eski alan: UrunTanim.AlisFiyati
        public string PurchaseCurrencyCode { get; set; }  //Alis doviz kodu. | Eski alan: UrunTanim.AlisDovizKodu
        public string PurchaseVatIncluded { get; set; }  //Alis KDV durumu: D=Dahil, H=Haric. | Eski alan: UrunTanim.AlisKdvDH
        public decimal PreviousSalePrice { get; set; }  //Onceki satis fiyati (indirim hesabi). | Eski alan: UrunTanim.OncekiSatisFiyati
        public decimal SalePrice { get; set; }  //Satis fiyati. | Eski alan: UrunTanim.SatisFiyati
        public decimal InstallmentSalePrice { get; set; }  //Taksitli satis fiyati. | Eski alan: UrunTanim.TaksitliSatisFiyati
        public string SaleCurrencyCode { get; set; }  //Satis doviz kodu. | Eski alan: UrunTanim.SatisDovizKodu
        public string SaleVatIncluded { get; set; }  //Satis KDV durumu: D=Dahil, H=Haric. | Eski alan: UrunTanim.SatisKdvDH
        public string ProductDetay { get; set; }  //Urun detay HTML/text. | Eski alan: UrunTanim.UrunDetay
        public string VideoUrl { get; set; }  //Urun video URL. | Eski alan: UrunTanim.VideoUrl
        public string IntegrationName { get; set; }  //Harici entegrasyon adi. | Eski alan: UrunTanim.EntegrasyonAdi
        public long? IntegrationId { get; set; }  //Harici entegrasyon urun ID. | Eski alan: UrunTanim.EntegrasyonId
        public bool IsInternetSaleActive { get; set; }  //E-ticarette satisa acik mi? | Eski alan: UrunTanim.InternetSatisAktif
        public bool IsTrendyolActive { get; set; }  //Trendyol entegrasyonu aktif mi? | Eski alan: UrunTanim.TrendyolAktif
        public bool IsHepsiBuradaActive { get; set; }  //Hepsiburada entegrasyonu aktif mi? | Eski alan: UrunTanim.HepsiburadaAktif
        public int Width { get; set; }  //Urun genisligi (cm). | Eski alan: UrunTanim.Genislik
        public int Height { get; set; }  //Urun yuksekligi (cm). | Eski alan: UrunTanim.Yukseklik
        public int Derinlik { get; set; }  //Urun derinligi (cm). | Eski alan: UrunTanim.Derinlik
        public decimal Desi { get; set; }  //Desi degeri (kargo). | Eski alan: UrunTanim.Desi
        public decimal Agirlik { get; set; }  //Agirlik (kg). | Eski alan: UrunTanim.Agirlik
        public string Color { get; set; }  //Randevu takvim renk kodu. | Eski alan: UrunTanim.Renk
        public bool? IsAppointmentActive { get; set; }  //Randevu modulu aktif mi? | Eski alan: UrunTanim.RandevuAktif
        public bool? IsAppointmentOpen { get; set; }  //Randevu almaya acik mi? | Eski alan: UrunTanim.RandevuAcik
        public string TrendyolSyncType { get; set; }  //Trendyol senkron tipi (I/U/D). | Eski alan: UrunTanim.TransTypeTrendyol
        public string HepsiBuradaSyncType { get; set; }  //Hepsiburada senkron tipi. | Eski alan: UrunTanim.TransTypeHepsiburada
        public DateTime? InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: UrunTanim.InsertDateTime
        public long? InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: UrunTanim.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: UrunTanim.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: UrunTanim.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: UrunTanim.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: UrunTanim.DeleteUser
        public DateTime? RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: UrunTanim.RecDateTime
        public string HepsiBuradaProductId { get; set; }  //Hepsiburada urun ID. | Eski alan: UrunTanim.HepsiBuradaUrunId
        public int MinSaleQuantity { get; set; }  //Minimum satis miktari. | Eski alan: UrunTanim.MinSatisMiktar
        public virtual ICollection<TradeDocumentLine> TradeDocumentLine { get; set; } = new List<TradeDocumentLine>();
    }
}
