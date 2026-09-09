using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Inventory
{
    public class Store  //Eski tablo: DepoTanim (TICARI_SLAVE1). Yeni şema: inventory.Store
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: DepoTanim.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: DepoTanim.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: DepoTanim.SirketTanimId
        public bool Stat { get; set; }  //Depo aktif mi? | Eski alan: DepoTanim.Aktif
        public bool IsEcommerceSaleActive { get; set; }  //E-ticaret satisinda stok gosterilsin mi? | Eski alan: DepoTanim.ETicaretSatisAktif
        public bool IsMainStore { get; set; }  //Ana depo mu? | Eski alan: DepoTanim.AnaDepo
        public string StoreCode { get; set; }  //Depo kodu. | Eski alan: DepoTanim.DepoKodu
        public string Description { get; set; }  //Depo aciklamasi. | Eski alan: DepoTanim.Aciklama
        public string AuthorizedPerson { get; set; }  //Depo yetkilisi. | Eski alan: DepoTanim.Yetkili
        public string Label { get; set; }  //Etiket. | Eski alan: DepoTanim.Etiket
        public string Address { get; set; }  //Adres. | Eski alan: DepoTanim.Adres
        public string City { get; set; }  //Il. | Eski alan: DepoTanim.Il
        public string District { get; set; }  //Ilce. | Eski alan: DepoTanim.Ilce
        public string Phone1 { get; set; }  //Birincil telefon. | Eski alan: DepoTanim.Telefon1
        public string Phone2 { get; set; }  //Ikincil telefon. | Eski alan: DepoTanim.Telefon2
        public string Fax { get; set; }  //Faks. | Eski alan: DepoTanim.Faks
        public DateTime? InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: DepoTanim.InsertDateTime
        public long? InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: DepoTanim.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: DepoTanim.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: DepoTanim.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: DepoTanim.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: DepoTanim.DeleteUser
        public DateTime? RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: DepoTanim.RecDateTime

        public virtual Company Company { get; set; }
        public virtual ICollection<CashRegister> CashRegister { get; set; } = new List<CashRegister>();
        public virtual ICollection<CounterReference> CounterReference { get; set; } = new List<CounterReference>();
        public virtual ICollection<StorePersonal> StorePersonal { get; set; } = new List<StorePersonal>();
        public virtual ICollection<TradeDocument> TradeDocument { get; set; } = new List<TradeDocument>();
        public virtual ICollection<TradeDocumentLine> TradeDocumentLine { get; set; } = new List<TradeDocumentLine>();
    }
}
