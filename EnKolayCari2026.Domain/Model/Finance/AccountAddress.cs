using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Finance
{
    public class AccountAddress  //Eski tablo: CariTanimAdres (TICARI_SLAVE1). Yeni şema: finance.AccountAddress
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: CariTanimAdres.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: CariTanimAdres.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: CariTanimAdres.SirketTanimId
        public long AccountId { get; set; }  //Bagli cari (FK -> CariTanim.Id). | Eski alan: CariTanimAdres.CariTanimId
        public string AddressTitle { get; set; }  //Adres basligi (Ev, Is vb.). | Eski alan: CariTanimAdres.AdresBaslik
        public string AdSoyad { get; set; }  //Teslim alacak kisi ad soyad. | Eski alan: CariTanimAdres.AdSoyad
        public string Address { get; set; }  //Acik adres. | Eski alan: CariTanimAdres.Adres
        public string City { get; set; }  //Il. | Eski alan: CariTanimAdres.Il
        public string District { get; set; }  //Ilce. | Eski alan: CariTanimAdres.Ilce
        public string PostalCode { get; set; }  //Posta kodu. | Eski alan: CariTanimAdres.PostaKodu
        public string Email { get; set; }  //E-posta. | Eski alan: CariTanimAdres.EMail
        public string Phone { get; set; }  //Telefon. | Eski alan: CariTanimAdres.Telefon
        public long? InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: CariTanimAdres.InsertUser
        public DateTime? InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: CariTanimAdres.InsertDateTime
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: CariTanimAdres.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: CariTanimAdres.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: CariTanimAdres.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: CariTanimAdres.DeleteUser
        public DateTime RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: CariTanimAdres.RecDateTime

        public virtual Company Company { get; set; }
        public virtual Account Account { get; set; }
    }
}
