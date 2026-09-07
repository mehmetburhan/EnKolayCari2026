using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Inventory
{
    public class Brand  //Eski tablo: MarkaTanim (TICARI_SLAVE1). Yeni şema: inventory.Brand
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: MarkaTanim.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: MarkaTanim.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: MarkaTanim.SirketTanimId
        public bool Stat { get; set; }  //Aktif mi? | Eski alan: MarkaTanim.Aktif
        public string BrandCode { get; set; }  //Marka kodu. | Eski alan: MarkaTanim.MarkaKodu
        public string Description { get; set; }  //Marka aciklamasi. | Eski alan: MarkaTanim.Aciklama
        public long? TrendyolId { get; set; }  //Trendyol marka ID. | Eski alan: MarkaTanim.TrendyolId
        public string TrendyolDescription { get; set; }  //Trendyol marka aciklamasi. | Eski alan: MarkaTanim.TrendyolAciklama
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: MarkaTanim.InsertUser
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: MarkaTanim.InsertDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: MarkaTanim.UpdateUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: MarkaTanim.UpdateDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: MarkaTanim.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: MarkaTanim.DeleteDateTime
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: MarkaTanim.RecDateTime

        public virtual Company Company { get; set; }
    }
}
