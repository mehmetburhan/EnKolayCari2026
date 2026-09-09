using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Finance
{
    public class Account  //Eski tablo: CariTanim (TICARI_SLAVE1). Yeni şema: finance.Account
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: CariTanim.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: CariTanim.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: CariTanim.SirketTanimId
        public int AccountType { get; set; }  //0=Musteri, 1=Toptanci, 2=Banka | Eski alan: CariTanim.CariTipi
        public bool? Stat { get; set; }  //Cari kaydi aktif mi? | Eski alan: CariTanim.Aktif
        public string AccountCode { get; set; }  //Cari hesap kodu. | Eski alan: CariTanim.CariKodu
        public string FirstName { get; set; }  //Ad (bireysel musteri). | Eski alan: CariTanim.Ad
        public string LastName { get; set; }  //Soyad (bireysel musteri). | Eski alan: CariTanim.Soyad
        public string FullNameOrTitle { get; set; }  //Ad soyad veya firma unvani. | Eski alan: CariTanim.AdSoyadUnvan
        public string BankAccountHolder { get; set; }  //Banka hesap sahibi adi. | Eski alan: CariTanim.BankaHesapSahibi
        public string Email { get; set; }  //E-posta adresi / e-ticaret giris. | Eski alan: CariTanim.EMail
        public string Password { get; set; }  //E-ticaret sifresi. | Eski alan: CariTanim.Parola
        public string CorrespondenceEmail { get; set; }  //Yazisma/bildirim e-posta adresi. | Eski alan: CariTanim.YazismaEMail
        public bool IsActivationCompleted { get; set; }  //E-posta aktivasyonu tamamlandi mi? | Eski alan: CariTanim.AktivasyonYapildi
        public string WebsiteUrl { get; set; }  //Web sitesi URL. | Eski alan: CariTanim.WebSayfasi
        public string Address { get; set; }  //Acik adres. | Eski alan: CariTanim.Adres
        public string City { get; set; }  //Il adi. | Eski alan: CariTanim.Il
        public string District { get; set; }  //Ilce adi. | Eski alan: CariTanim.Ilce
        public string PostalCode { get; set; }  //Posta kodu. | Eski alan: CariTanim.PostaKodu
        public string TaxOffice { get; set; }  //Vergi dairesi. | Eski alan: CariTanim.VergiDairesi
        public string TaxNumber { get; set; }  //Vergi kimlik numarasi (VKN). | Eski alan: CariTanim.VergiNumarasi
        public string Phone1 { get; set; }  //Birincil telefon. | Eski alan: CariTanim.Telefon1
        public string Phone2 { get; set; }  //Ikincil telefon. | Eski alan: CariTanim.Telefon2
        public string Fax { get; set; }  //Faks numarasi. | Eski alan: CariTanim.Faks
        public string BankBranchCode { get; set; }  //Banka sube kodu. | Eski alan: CariTanim.BankaSubeKodu
        public string AccountNumber { get; set; }  //Banka hesap numarasi. | Eski alan: CariTanim.HesapNo
        public string IBAN { get; set; }  //IBAN numarasi. | Eski alan: CariTanim.IBAN
        public bool? IsETaxpayerActive { get; set; }  //e-Mukellef (e-Fatura/e-Arsiv) aktif mi? | Eski alan: CariTanim.EMukellefAktif
        public decimal DiscountRate { get; set; }  //Varsayilan iskonto orani (%). | Eski alan: CariTanim.IskontoOrani
        public long? PriceGroupId { get; set; }  //Bagli fiyat grubu (FK -> FiyatGrupTanim.Id). | Eski alan: CariTanim.FiyatGrupTanimId
        public string IntegrationName { get; set; }  //Harici entegrasyon adi (Trendyol vb.). | Eski alan: CariTanim.EntegrasyonAdi
        public string IntegrationId { get; set; }  //Harici entegrasyon cari ID. | Eski alan: CariTanim.EntegrasyonId
        public string Label { get; set; }  //Etiket listesi (virgul/noktali virgul ile). | Eski alan: CariTanim.Etiket
        public bool? ShowIbanOnEcommerce { get; set; }  //E-ticarette IBAN gosterilsin mi? | Eski alan: CariTanim.ETicaretIBANGoster
        public bool NewsletterOptIn { get; set; }  //E-bulten almak istiyor mu? | Eski alan: CariTanim.EBulten
        public bool SmsOptIn { get; set; }  //SMS almak istiyor mu? | Eski alan: CariTanim.SmsAlmakIstiyorum
        public bool MembershipAgreementAccepted { get; set; }  //Uyelik sozlesmesi onayi. | Eski alan: CariTanim.UyelikSozlesmesiniOkudum
        public bool KvkkAccepted { get; set; }  //KVKK metni onayi. | Eski alan: CariTanim.KvkkOkudum
        public int RecordSource { get; set; }  //0=Normal, 1=ETicaret | Eski alan: CariTanim.KayitYeri
        public string NationalId { get; set; }  //TC kimlik numarasi. | Eski alan: CariTanim.TCKimlikNo
        public string Gender { get; set; }  //K=Kadin, E=Erkek, C=Cocuk | Eski alan: CariTanim.Cinsiyet
        public DateTime? BirthDate { get; set; }  //Dogum tarihi. | Eski alan: CariTanim.DogumTarihi
        public bool FreeShipping { get; set; }  //Bu cariye ucretsiz kargo uygulansin mi? | Eski alan: CariTanim.KargoBedava
        public long? TrendyolId { get; set; }  //Trendyol musteri ID. | Eski alan: CariTanim.TrendyolId
        public string TrendyolDescription { get; set; }  //Trendyol aciklama/not. | Eski alan: CariTanim.TrendyolAciklama
        public int PaymentTermDays { get; set; }  //Varsayilan vade gunu. | Eski alan: CariTanim.VadeGun
        public string SocialSecurity { get; set; }  //Sosyal guvence bilgisi. | Eski alan: CariTanim.SosyalGuvence
        public string AdditionalInfo { get; set; }  //Ek bilgi/not alani. | Eski alan: CariTanim.EkBilgi
        public string KvkkApprovalCode { get; set; }  //KVKK SMS/e-posta onay kodu. | Eski alan: CariTanim.KVKKOnayKodu
        public long? InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: CariTanim.InsertUser
        public DateTime? RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: CariTanim.RecDateTime
        public DateTime? InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: CariTanim.InsertDateTime
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: CariTanim.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: CariTanim.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: CariTanim.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: CariTanim.DeleteUser

        public virtual Company Company { get; set; }
        public virtual ICollection<TradeDocumentTemp> TradeDocumentTemp { get; set; } = new List<TradeDocumentTemp>();
        public virtual ICollection<AccountTransaction> AccountTransaction { get; set; } = new List<AccountTransaction>();
        public virtual ICollection<CheckNote> CheckNote { get; set; } = new List<CheckNote>();
        public virtual ICollection<CheckNoteTransaction> CheckNoteTransaction { get; set; } = new List<CheckNoteTransaction>();
        public virtual ICollection<TradeDocument> TradeDocument { get; set; } = new List<TradeDocument>();
        public virtual ICollection<AccountAddress> AccountAddress { get; set; } = new List<AccountAddress>();
        public virtual ICollection<AccountDocument> AccountDocument { get; set; } = new List<AccountDocument>();
    }
}
