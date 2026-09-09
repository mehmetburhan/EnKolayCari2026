using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class PersonalCompany  //AI platform tablosu. Personel–sirket uyelik / login yetkileri (web/mobil).
    {
        public long Id { get; set; }  //Primary key.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public long PersonalId { get; set; }  //FK → Personal.Id.
        public long CompanyId { get; set; }  //FK → Company.Id.
        public bool IsWebLogin { get; set; }  //Web giris izni.
        public bool IsMobileLogin { get; set; }  //Mobil giris izni.
        public bool ShowAllAuditSession { get; set; }  //Tum denetim oturumlarini gorebilsin (urun bayragi).
        public bool ShowAllAuditAction { get; set; }  //Tum denetim aksiyonlarini gorebilsin.
        public bool BranchActionReport { get; set; }  //Sube aksiyon raporu yetkisi.
        public DateTime CreatedDate { get; set; }  //Olusturma tarihi.
        public long CreatedUser { get; set; }  //Olusturan kullanici Id.
        public DateTime? ModifedDate { get; set; }  //Guncelleme tarihi.
        public long? ModifedUser { get; set; }  //Guncelleyen kullanici Id.
        public DateTime? DeletedDate { get; set; }  //Soft-delete tarihi.
        public long? DeletedUser { get; set; }  //Soft-delete yapan kullanici Id.
        public long IsDelete { get; set; }  //Soft-delete bayragi (bigint).

        public virtual Personal Personal { get; set; }
        public virtual Company Company { get; set; }
    }
}
