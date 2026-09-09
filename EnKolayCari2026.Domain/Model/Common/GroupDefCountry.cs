using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class GroupDefCountry  //AI platform tablosu. Grup–ulke eslemesi.
    {
        public long Id { get; set; }  //Primary key.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public bool Stat { get; set; }  //Aktif/pasif.
        public long CompanyId { get; set; }  //Sirket Id.
        public long GroupDefId { get; set; }  //FK → GroupDef.Id.
        public string CountryCode { get; set; }  //Ulke kodu.
        public DateTime CreatedDate { get; set; }  //Olusturma tarihi.
        public long CreatedUser { get; set; }  //Olusturan kullanici Id.
        public DateTime? ModifedDate { get; set; }  //Guncelleme tarihi.
        public long? ModifedUser { get; set; }  //Guncelleyen kullanici Id.
        public DateTime? DeletedDate { get; set; }  //Soft-delete tarihi.
        public long? DeletedUser { get; set; }  //Soft-delete yapan kullanici Id.
        public long IsDelete { get; set; }  //Soft-delete bayragi (bigint).

        public virtual Company Company { get; set; }
        public virtual GroupDef GroupDef { get; set; }
    }
}
