using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class LabelPool  //AI platform tablosu. Kayit–etiket eslemesi (TableName + TableId + Label). Eski tablo: EtiketHavuzu.
    {
        public long Id { get; set; }  //Primary key.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public long CompanyId { get; set; }  //Sirket Id.
        public string TableName { get; set; }  //Kaynak tablo adi.
        public long TableId { get; set; }  //Kaynak kayit Id.
        public string Label { get; set; }  //Etiket metni / kodu.
        public long? LabelDefId { get; set; }  //FK → LabelDef.Id (opsiyonel).
        public DateTime CreatedDate { get; set; }  //Olusturma tarihi.
        public long CreatedUser { get; set; }  //Olusturan kullanici Id.
        public DateTime? ModifedDate { get; set; }  //Guncelleme tarihi.
        public long? ModifedUser { get; set; }  //Guncelleyen kullanici Id.
        public DateTime? DeletedDate { get; set; }  //Soft-delete tarihi.
        public long? DeletedUser { get; set; }  //Soft-delete yapan kullanici Id.
        public bool IsDelete { get; set; }  //Soft-delete bayragi (bit).

        public virtual LabelDef LabelDef { get; set; }
    }
}
