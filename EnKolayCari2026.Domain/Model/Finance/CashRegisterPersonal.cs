using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Finance
{
    public class CashRegisterPersonal  //Eski tablo: KasaPersonel (TICARI_SLAVE1). Yeni şema: finance.CashRegisterPersonal
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: KasaPersonel.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: KasaPersonel.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: KasaPersonel.SirketTanimId
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: KasaPersonel.InsertUser
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: KasaPersonel.InsertDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: KasaPersonel.UpdateUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: KasaPersonel.UpdateDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: KasaPersonel.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: KasaPersonel.DeleteDateTime
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: KasaPersonel.RecDateTime
        public long CashRegisterId { get; set; }  //Kasa (FK -> KasaTanim.Id). | Eski alan: KasaPersonel.KasaId
        public long PersonalId { get; set; }  //Personel (FK -> PersonelTanim.Id). | Eski alan: KasaPersonel.PersonelId

        public virtual Company Company { get; set; }
        public virtual CashRegister CashRegister { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
