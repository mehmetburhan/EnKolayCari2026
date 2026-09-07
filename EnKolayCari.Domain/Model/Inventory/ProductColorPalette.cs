using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Inventory
{
    public class ProductColorPalette  //Eski tablo: UrunRenkPaleti (TICARI_SLAVE1). Yeni şema: inventory.ProductColorPalette
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: UrunRenkPaleti.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: UrunRenkPaleti.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: UrunRenkPaleti.SirketTanimId
        public bool Stat { get; set; }  //Aktif mi? | Eski alan: UrunRenkPaleti.Aktif
        public int SortOrder { get; set; }  //Listeleme sirasi. | Eski alan: UrunRenkPaleti.Sira
        public string ColorCode { get; set; }  //Renk kodu (hex veya kisa kod). | Eski alan: UrunRenkPaleti.RenkKodu
        public string ColorDescription { get; set; }  //Renk aciklamasi. | Eski alan: UrunRenkPaleti.RenkAciklama
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: UrunRenkPaleti.InsertUser
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: UrunRenkPaleti.InsertDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: UrunRenkPaleti.UpdateUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: UrunRenkPaleti.UpdateDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: UrunRenkPaleti.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: UrunRenkPaleti.DeleteDateTime
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: UrunRenkPaleti.RecDateTime
    }
}
