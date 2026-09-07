using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class PersonalLoginActivity  //AI platform tablosu. Personel login / logout / basarisiz giris aktivite logu.
    {
        public long Id { get; set; }  //Primary key.
        public Guid GId { get; set; }  //Dis kimlik (Guid).
        public long PersonalId { get; set; }  //FK → Personal.Id.
        public string ActivityType { get; set; }  //Aktivite tipi (LOGIN, LOGOUT, FAIL...).
        public bool IsSuccess { get; set; }  //Basarili mi.
        public string CustomHeaderJson { get; set; }  //CustomHeader JSON snapshot.
        public string DeviceId { get; set; }  //Cihaz kimligi.
        public string ClientOs { get; set; }  //Istemci OS.
        public string AppVersion { get; set; }  //Uygulama surumu.
        public string Language { get; set; }  //Dil kodu.
        public string IpAddress { get; set; }  //IP adresi.
        public DateTime CreatedDate { get; set; }  //Aktivite zamani.

        public virtual Personal Personal { get; set; }
    }
}
