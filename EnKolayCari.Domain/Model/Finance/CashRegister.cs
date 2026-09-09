using System;
using EnKolayCari.Domain.Model.Common;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Finance
{
    public class CashRegister  //Eski tablo: KasaTanim (TICARI_SLAVE1). Yeni şema: finance.CashRegister
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: KasaTanim.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: KasaTanim.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: KasaTanim.SirketTanimId
        public bool Stat { get; set; }  //Kasa aktif mi? | Eski alan: KasaTanim.Aktif
        public string CashRegisterCode { get; set; }  //Kasa kodu. | Eski alan: KasaTanim.KasaKodu
        public string Description { get; set; }  //Kasa aciklamasi. | Eski alan: KasaTanim.Aciklama
        public long StoreId { get; set; }  //Bagli depo (FK -> DepoTanim.Id). | Eski alan: KasaTanim.DepoId
        public DateTime? InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: KasaTanim.InsertDateTime
        public long? InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: KasaTanim.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: KasaTanim.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: KasaTanim.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: KasaTanim.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: KasaTanim.DeleteUser
        public DateTime? RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: KasaTanim.RecDateTime

        public virtual Company Company { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<AccountTransaction> AccountTransaction { get; set; } = new List<AccountTransaction>();
        public virtual ICollection<CashRegisterPersonal> CashRegisterPersonal { get; set; } = new List<CashRegisterPersonal>();
        public virtual ICollection<CashTransaction> CashTransaction { get; set; } = new List<CashTransaction>();
    }
}
