using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Inventory
{
    public class StorePersonal  //Eski tablo: DepoPersonel (TICARI_SLAVE1). Yeni şema: inventory.StorePersonal
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: DepoPersonel.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: DepoPersonel.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: DepoPersonel.SirketTanimId
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: DepoPersonel.InsertUser
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: DepoPersonel.InsertDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: DepoPersonel.UpdateUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: DepoPersonel.UpdateDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: DepoPersonel.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: DepoPersonel.DeleteDateTime
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: DepoPersonel.RecDateTime
        public long StoreId { get; set; }  //Depo (FK -> DepoTanim.Id). | Eski alan: DepoPersonel.DepoId
        public long PersonalId { get; set; }  //Personel (FK -> TICARI_MASTER.dbo.PersonelTanim.Id). | Eski alan: DepoPersonel.PersonelId

        public virtual Company Company { get; set; }
        public virtual Store Store { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
