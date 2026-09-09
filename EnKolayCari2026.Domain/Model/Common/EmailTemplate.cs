using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class EmailTemplate  //Eski tablo: EMailTanim (TICARI_SLAVE1). Yeni şema: common.EmailTemplate
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: EMailTanim.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: EMailTanim.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: EMailTanim.SirketTanimId
        public bool Stat { get; set; }  //Sablon aktif mi? | Eski alan: EMailTanim.Aktif
        public string Konu { get; set; }  //E-posta konusu. | Eski alan: EMailTanim.Konu
        public string Body { get; set; }  //E-posta govde metni (HTML/text). | Eski alan: EMailTanim.Metin
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: EMailTanim.InsertDateTime
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: EMailTanim.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: EMailTanim.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: EMailTanim.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: EMailTanim.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: EMailTanim.DeleteUser
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: EMailTanim.RecDateTime

        public virtual Company Company { get; set; }
    }
}
