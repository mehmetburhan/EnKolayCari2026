using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class PersonalWidget  //AI platform tablosu. Personel dashboard widget tercihleri.
    {
        public long Id { get; set; }  //Primary key.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public bool Stat { get; set; }  //Aktif/pasif.
        public long PersonalId { get; set; }  //FK → Personal.Id.
        public string WidgetId { get; set; }  //Widget kodu.
        public string With { get; set; }  //Genislik / layout parametresi (kolon adi: With).
        public bool IsVisible { get; set; }  //Gorunur mu.
        public int Position { get; set; }  //Sira pozisyonu.
        public int RowIndex { get; set; }  //Satir indeksi.
        public DateTime CreatedDate { get; set; }  //Olusturma tarihi.
        public long CreatedUser { get; set; }  //Olusturan kullanici Id.
        public DateTime? ModifedDate { get; set; }  //Guncelleme tarihi.
        public long? ModifedUser { get; set; }  //Guncelleyen kullanici Id.
        public DateTime? DeletedDate { get; set; }  //Soft-delete tarihi.
        public long? DeletedUser { get; set; }  //Soft-delete yapan kullanici Id.
        public long IsDelete { get; set; }  //Soft-delete bayragi (bigint).

        public virtual Personal Personal { get; set; }
    }
}
