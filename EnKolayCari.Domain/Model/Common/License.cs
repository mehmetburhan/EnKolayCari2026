using System;
using EnKolayCari.Domain.Model.Dbo;
using EnKolayCari.Domain.Model.Finance;
using EnKolayCari.Domain.Model.HangFire;
using EnKolayCari.Domain.Model.Inventory;
using EnKolayCari.Domain.Model.Report;
using EnKolayCari.Domain.Model.Trade;

namespace EnKolayCari.Domain.Model.Common
{
    public class License  //Eski tablo: Lisans (TICARI_MASTER). Yeni şema: common.License
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: Lisans.Id
        public Guid? GId { get; set; }  //Global benzersiz kimlik. | Eski alan: Lisans.GId
        public long CompanyId { get; set; }  //Bagli sirket (FK). | Eski alan: Lisans.SirketTanimId
        public bool? IsGift { get; set; }  //Hediye/demo lisans mi? | Eski alan: Lisans.Hediye
        public string LicenseType { get; set; }  //Lisans paket kodu (FK -> LisansTipi.Kod). | Eski alan: Lisans.LisansTipi
        public DateTime StartDate { get; set; }  //Lisans baslangic tarihi. | Eski alan: Lisans.BaslangicTarihi
        public DateTime EndDate { get; set; }  //Lisans bitis tarihi. | Eski alan: Lisans.BitisTarihi
        public int? CollectionMethod { get; set; }  //Tahsilat/odeme sekli kodu. | Eski alan: Lisans.TahsilatSekli
        public decimal CollectionAmount { get; set; }  //Lisans ucreti. | Eski alan: Lisans.TahsilatTutari
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: Lisans.InsertDateTime
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: Lisans.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: Lisans.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: Lisans.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: Lisans.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: Lisans.DeleteUser

        public virtual Company Company { get; set; }
    }
}
