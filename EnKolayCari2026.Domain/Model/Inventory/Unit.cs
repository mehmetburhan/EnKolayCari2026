using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Inventory
{
    public class Unit  //Eski tablo: BirimTanim (TICARI_SLAVE1). Yeni şema: inventory.Unit
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: BirimTanim.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: BirimTanim.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: BirimTanim.SirketTanimId
        public string Code { get; set; }  //Birim kodu. | Eski alan: BirimTanim.Kod
        public string Description { get; set; }  //Birim aciklamasi. | Eski alan: BirimTanim.Aciklama
        public int Hassasiyet { get; set; }  //Ondalik hassasiyet (hane sayisi). | Eski alan: BirimTanim.Hassasiyet
        public string Label { get; set; }  //Etiket/barkod metni. | Eski alan: BirimTanim.Etiket
        public DateTime? InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: BirimTanim.InsertDateTime
        public long? InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: BirimTanim.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: BirimTanim.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: BirimTanim.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: BirimTanim.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: BirimTanim.DeleteUser
        public DateTime? RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: BirimTanim.RecDateTime

        public virtual Company Company { get; set; }
        public virtual ICollection<TradeDocumentLine> TradeDocumentLine { get; set; } = new List<TradeDocumentLine>();
        public virtual ICollection<TradeDocumentLineTemp> TradeDocumentLineTemp { get; set; } = new List<TradeDocumentLineTemp>();
    }
}
