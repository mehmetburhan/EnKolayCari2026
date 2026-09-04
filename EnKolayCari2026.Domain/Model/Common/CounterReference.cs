using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class CounterReference  //Eski tablo: SayacReferans (TICARI_SLAVE1). Yeni şema: common.CounterReference
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: SayacReferans.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: SayacReferans.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: SayacReferans.SirketTanimId
        public string Type { get; set; }  //Sayac tipi. | Eski alan: SayacReferans.Tip
        public long CounterDefId { get; set; }  //Sayac (FK -> Sayac.Id). | Eski alan: SayacReferans.SayacTanimId
        public long StoreId { get; set; }  //Depo (FK -> DepoTanim.Id). | Eski alan: SayacReferans.DepoTanimId
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: SayacReferans.InsertUser
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: SayacReferans.InsertDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: SayacReferans.UpdateUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: SayacReferans.UpdateDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: SayacReferans.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: SayacReferans.DeleteDateTime
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: SayacReferans.RecDateTime

        public virtual Company Company { get; set; }
        public virtual Store Store { get; set; }
    }
}
