using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class AppSettings  //AI platform tablosu. Uygulama genel ayarlari (mobil magaza URL, sure sinirlari vb.).
    {
        public long Id { get; set; }  //Primary key.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public string AppCode { get; set; }  //Uygulama kodu (WEB/MOBILE/urun).
        public int MaxStayInsideHour { get; set; }  //Maksimum icerde kalma suresi (saat).
        public string IOSAppUrl { get; set; }  //iOS magaza / indir URL.
        public string AndroidAppUrl { get; set; }  //Android magaza / indir URL.
        public DateTime CreatedDate { get; set; }  //Olusturma tarihi.
        public long CreatedUser { get; set; }  //Olusturan kullanici Id.
        public DateTime? ModifedDate { get; set; }  //Guncelleme tarihi.
        public long? ModifedUser { get; set; }  //Guncelleyen kullanici Id.
        public DateTime? DeletedDate { get; set; }  //Soft-delete tarihi.
        public long? DeletedUser { get; set; }  //Soft-delete yapan kullanici Id.
        public bool IsDelete { get; set; }  //Soft-delete bayragi (bit).
    }
}
