using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Inventory
{
    public class ProductBarcode  //Eski tablo: UrunBarkod (TICARI_SLAVE1). Yeni şema: inventory.ProductBarcode
    {
        public long Id { get; set; }  //- | Eski alan: UrunBarkod.Id
        public Guid GId { get; set; }  //- | Eski alan: UrunBarkod.GId
        public long CompanyId { get; set; }  //- | Eski alan: UrunBarkod.SirketTanimId
        public long ProductId { get; set; }  //- | Eski alan: UrunBarkod.UrunTanimId
        public int SortOrder { get; set; }  //- | Eski alan: UrunBarkod.Sira
        public bool IsMainProduct { get; set; }  //- | Eski alan: UrunBarkod.AnaUrun
        public string ColorSize { get; set; }  //- | Eski alan: UrunBarkod.RenkBeden
        public string Size { get; set; }  //- | Eski alan: UrunBarkod.Beden
        public string Color { get; set; }  //- | Eski alan: UrunBarkod.Renk
        public long? ProductColorPaletteId { get; set; }  //- | Eski alan: UrunBarkod.UrunRenkPaletiId
        public string Barcode { get; set; }  //- | Eski alan: UrunBarkod.Barkod
        public bool IsSalePriceActive { get; set; }  //- | Eski alan: UrunBarkod.SatisFiyatAktif
        public decimal SalePrice { get; set; }  //- | Eski alan: UrunBarkod.SatisFiyati
        public decimal InstallmentSalePrice { get; set; }  //- | Eski alan: UrunBarkod.TaksitliSatisFiyati
        public decimal? PreviousSalePrice { get; set; }  //- | Eski alan: UrunBarkod.OncekiSatisFiyati
        public long InsertUser { get; set; }  //- | Eski alan: UrunBarkod.InsertUser
        public DateTime InsertDateTime { get; set; }  //- | Eski alan: UrunBarkod.InsertDateTime
        public long? UpdateUser { get; set; }  //- | Eski alan: UrunBarkod.UpdateUser
        public DateTime? UpdateDateTime { get; set; }  //- | Eski alan: UrunBarkod.UpdateDateTime
        public long? DeleteUser { get; set; }  //- | Eski alan: UrunBarkod.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //- | Eski alan: UrunBarkod.DeleteDateTime
        public DateTime RecordDateTime { get; set; }  //- | Eski alan: UrunBarkod.RecDateTime
        public string HepsiBuradaProductId { get; set; }  //- | Eski alan: UrunBarkod.HepsiBuradaUrunId
    }
}
