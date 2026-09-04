using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class LabelDef  //AI platform tablosu. Etiket (tag) tanimlari / kategorileri. Eski tablo: EtiketTanim.
    {
        public long Id { get; set; }  //Primary key.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public long CompanyId { get; set; }  //Sirket Id.
        public bool Stat { get; set; }  //Aktif/pasif.
        public string Label { get; set; }  //Etiket metni.
        public string LabelDescription { get; set; }  //Etiket aciklamasi.
        public string SeoKey { get; set; }  //SEO / slug anahtari.
        public bool? IsMainCategory { get; set; }  //Ana kategori mi.
        public int? Order { get; set; }  //Siralama.
        public DateTime CreatedDate { get; set; }  //Olusturma tarihi.
        public long CreatedUser { get; set; }  //Olusturan kullanici Id.
        public DateTime? ModifedDate { get; set; }  //Guncelleme tarihi.
        public long? ModifedUser { get; set; }  //Guncelleyen kullanici Id.
        public DateTime? DeletedDate { get; set; }  //Soft-delete tarihi.
        public long? DeletedUser { get; set; }  //Soft-delete yapan kullanici Id.
        public bool IsDelete { get; set; }  //Soft-delete bayragi (bit).
        public virtual ICollection<LabelPool> LabelPool { get; set; } = new List<LabelPool>();
    }
}
