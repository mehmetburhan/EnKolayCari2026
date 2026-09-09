using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class Branch  //AI platform tablosu. Sube / magaza / lokasyon tanimi. CompanyMasterCode ile tenant baglidir.
    {
        public long Id { get; set; }  //Primary key.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public bool Stat { get; set; }  //Aktif/pasif.
        public string CompanyMasterCode { get; set; }  //Sirket master kodu (Company.CompanyMasterCode).
        public string Code { get; set; }  //Sube kodu; (CompanyMasterCode, Code, DeletedDate) unique.
        public string Description { get; set; }  //Sube adi / aciklama.
        public long? ManagerPersonalId { get; set; }  //Sube muduru Personal.Id (opsiyonel).
        public string ManagerAccountCode { get; set; }  //Sube muduru hesap/kod referansi (legacy/entegrasyon).
        public string Country { get; set; }  //Ulke adi veya kod metni (gosterim).
        public string City { get; set; }  //Sehir adi (gosterim).
        public string District { get; set; }  //Ilce / semt.
        public string CityCode { get; set; }  //Sehir kodu (City.FullCityCode / CityCode baglantisi).
        public string DistrictCode { get; set; }  //Ilce kodu.
        public decimal Latitude { get; set; }  //Enlem (GPS).
        public decimal Longitude { get; set; }  //Boylam (GPS).
        public string StartIP { get; set; }  //Izinli IP araligi baslangici (opsiyonel).
        public string EndIP { get; set; }  //Izinli IP araligi bitisi (opsiyonel).
        public string Color { get; set; }  //UI renk / badge JSON veya hex.
        public string Label { get; set; }  //Etiket / serbest metin veya JSON label listesi.
        public DateTime CreatedDate { get; set; }  //Olusturma tarihi.
        public long CreatedUser { get; set; }  //Olusturan kullanici Id.
        public DateTime? ModifedDate { get; set; }  //Guncelleme tarihi (Modifed typo korunur).
        public long? ModifedUser { get; set; }  //Guncelleyen kullanici Id.
        public DateTime? DeletedDate { get; set; }  //Soft-delete tarihi.
        public long? DeletedUser { get; set; }  //Soft-delete yapan kullanici Id.
        public long IsDelete { get; set; }  //Soft-delete bayragi (bigint).
        public virtual ICollection<PersonalBranch> PersonalBranch { get; set; } = new List<PersonalBranch>();
    }
}
