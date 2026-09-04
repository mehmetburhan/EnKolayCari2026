using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class License  //Eski tablo: Lisans (TICARI_MASTER). Yeni şema: common.License
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: Lisans.Id
        public Guid? GId { get; set; }  //Global benzersiz kimlik. | Eski alan: Lisans.GId
        public long CompanyId { get; set; }  //Bagli sirket (FK). | Eski alan: Lisans.SirketTanimId
        public bool? Hediye { get; set; }  //Hediye/demo lisans mi? | Eski alan: Lisans.Hediye
        public string LicenseType { get; set; }  //Lisans paket kodu (FK -> LisansTipi.Kod). | Eski alan: Lisans.LisansTipi
        public DateTime StartDate { get; set; }  //Lisans baslangic tarihi. | Eski alan: Lisans.BaslangicTarihi
        public DateTime EndDate { get; set; }  //Lisans bitis tarihi. | Eski alan: Lisans.BitisTarihi
        public int? TahsilatSekli { get; set; }  //Tahsilat/odeme sekli kodu. | Eski alan: Lisans.TahsilatSekli
        public decimal TahsilatTutari { get; set; }  //Lisans ucreti. | Eski alan: Lisans.TahsilatTutari
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: Lisans.InsertDateTime
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: Lisans.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: Lisans.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: Lisans.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: Lisans.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: Lisans.DeleteUser

        public virtual Company Company { get; set; }
    }
}
