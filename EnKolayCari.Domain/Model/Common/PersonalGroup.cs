using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class PersonalGroup  //AI platform tablosu. Personel grup kodu eslemesi (GroupCode int).
    {
        public long Id { get; set; }  //Satir Id.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public long? CompanyId { get; set; }  //Sirket Id.
        public long PersonalId { get; set; }  //FK → Personal.Id.
        public int GroupCode { get; set; }  //Grup kodu (int).
        public DateTime CreatedDate { get; set; }  //Olusturma tarihi.
        public long CreatedUser { get; set; }  //Olusturan kullanici Id.
        public DateTime? ModifedDate { get; set; }  //Guncelleme tarihi.
        public long? ModifedUser { get; set; }  //Guncelleyen kullanici Id.
        public DateTime? DeletedDate { get; set; }  //Soft-delete tarihi.
        public long? DeletedUser { get; set; }  //Soft-delete yapan kullanici Id.
        public long IsDelete { get; set; }  //Soft-delete bayragi (bigint).

        public virtual Company Company { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
