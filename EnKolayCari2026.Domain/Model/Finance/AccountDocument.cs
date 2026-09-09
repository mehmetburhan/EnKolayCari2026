using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Finance
{
    public class AccountDocument  //Eski tablo: CariTanimBelge (TICARI_SLAVE1). Yeni şema: finance.AccountDocument
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: CariTanimBelge.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: CariTanimBelge.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: CariTanimBelge.SirketTanimId
        public long DocumentDefId { get; set; }  //Belge sablonu (FK -> BelgeTanim.Id). | Eski alan: CariTanimBelge.BelgeTanimId
        public string DocumentTanimDescription { get; set; }  //Belge aciklamasi (anlik kopya). | Eski alan: CariTanimBelge.BelgeTanimAciklama
        public long AccountId { get; set; }  //Bagli cari (FK -> CariTanim.Id). | Eski alan: CariTanimBelge.CariTanimId
        public string DocumentIcerik { get; set; }  //Imzalanan belge icerigi. | Eski alan: CariTanimBelge.BelgeIcerik
        public string ApprovalType { get; set; }  //Onay tipi kodu. | Eski alan: CariTanimBelge.OnayTipi
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: CariTanimBelge.InsertUser
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: CariTanimBelge.InsertDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: CariTanimBelge.UpdateUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: CariTanimBelge.UpdateDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: CariTanimBelge.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: CariTanimBelge.DeleteDateTime
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: CariTanimBelge.RecDateTime

        public virtual Company Company { get; set; }
        public virtual Account Account { get; set; }
    }
}
