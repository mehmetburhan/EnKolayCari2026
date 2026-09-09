using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Inventory
{
    public class ProductUnit  //Eski tablo: UrunBirim (TICARI_SLAVE1). Yeni şema: inventory.ProductUnit
    {
        public long Id { get; set; }  //- | Eski alan: UrunBirim.Id
        public Guid GId { get; set; }  //- | Eski alan: UrunBirim.GId
        public long CompanyId { get; set; }  //- | Eski alan: UrunBirim.SirketTanimId
        public bool Stat { get; set; }  //- | Eski alan: UrunBirim.Aktif
        public long ProductId { get; set; }  //- | Eski alan: UrunBirim.UrunId
        public long UnitId { get; set; }  //- | Eski alan: UrunBirim.BirimId
        public string Barcode { get; set; }  //- | Eski alan: UrunBirim.Barkod
        public double Carpan { get; set; }  //- | Eski alan: UrunBirim.Carpan
        public int Hassasiyet { get; set; }  //- | Eski alan: UrunBirim.Hassasiyet
        public string Label { get; set; }  //- | Eski alan: UrunBirim.Etiket
        public DateTime? InsertDateTime { get; set; }  //- | Eski alan: UrunBirim.InsertDateTime
        public long? InsertUser { get; set; }  //- | Eski alan: UrunBirim.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //- | Eski alan: UrunBirim.UpdateDateTime
        public long? UpdateUser { get; set; }  //- | Eski alan: UrunBirim.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //- | Eski alan: UrunBirim.DeleteDateTime
        public long? DeleteUser { get; set; }  //- | Eski alan: UrunBirim.DeleteUser
        public DateTime? RecordDateTime { get; set; }  //- | Eski alan: UrunBirim.RecDateTime
    }
}
