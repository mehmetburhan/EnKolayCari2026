using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class MailLog  //AI platform tablosu. Gonderilen e-posta logu (OTP, bildirim vb.).
    {
        public long Id { get; set; }  //Primary key.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public long CompanyId { get; set; }  //Sirket Id.
        public string TableName { get; set; }  //Ilgili tablo adi.
        public long TableId { get; set; }  //Ilgili kayit Id.
        public string MailTypeCode { get; set; }  //Mail tipi kodu (RESET_PASSWORD, INVITE...).
        public string MailTo { get; set; }  //Alici e-posta.
        public string MailSubject { get; set; }  //Konu.
        public string MailBody { get; set; }  //Govde (HTML/text).
        public DateTime SentDate { get; set; }  //Gonderim zamani.
        public long? SentByPersonalId { get; set; }  //Gonderen personel Id.
        public bool Stat { get; set; }  //Aktif/pasif.
        public DateTime CreatedDate { get; set; }  //Olusturma tarihi.
        public long CreatedUser { get; set; }  //Olusturan kullanici Id.
        public DateTime? ModifedDate { get; set; }  //Guncelleme tarihi.
        public long? ModifedUser { get; set; }  //Guncelleyen kullanici Id.
        public DateTime? DeletedDate { get; set; }  //Soft-delete tarihi.
        public long? DeletedUser { get; set; }  //Soft-delete yapan kullanici Id.
        public bool IsDelete { get; set; }  //Soft-delete bayragi (bit).

        public virtual Company Company { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
