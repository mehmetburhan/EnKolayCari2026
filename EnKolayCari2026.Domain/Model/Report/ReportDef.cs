using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Report
{
    public class ReportDef  //Eski tablo: RaporTanim (TICARI_SLAVE1). Yeni şema: report.ReportDef
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: RaporTanim.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: RaporTanim.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: RaporTanim.SirketTanimId
        public string ReportType { get; set; }  //Rapor tipi kodu. | Eski alan: RaporTanim.RaporTipi
        public string Description { get; set; }  //Rapor aciklamasi. | Eski alan: RaporTanim.Aciklama
        public byte[] BinData { get; set; }  //Rapor sablon binary verisi. | Eski alan: RaporTanim.BinData
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: RaporTanim.InsertDateTime
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: RaporTanim.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: RaporTanim.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: RaporTanim.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: RaporTanim.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: RaporTanim.DeleteUser
        public DateTime? RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: RaporTanim.RecDateTime

        public virtual Company Company { get; set; }
    }
}
