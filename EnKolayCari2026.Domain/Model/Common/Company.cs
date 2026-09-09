using System;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;
using EnKolayCari2026.Domain.Model.Trade;

namespace EnKolayCari2026.Domain.Model.Common
{
    public class Company  //Eski tablo: SirketTanim (TICARI_MASTER). Yeni şema: common.Company
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: SirketTanim.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: SirketTanim.GId
        public bool Stat { get; set; }  //Sirket kaydi aktif mi? | Eski alan: SirketTanim.Aktif
        public string Code { get; set; }  //Sirket kisa kodu. | Eski alan: SirketTanim.Kod
        public string City { get; set; }  //Il adi. | Eski alan: SirketTanim.Il
        public bool IsActivationCompleted { get; set; }  //E-posta aktivasyonu tamamlandi mi? | Eski alan: SirketTanim.AktivasyonYapildi
        public DateTime? ActivationDate { get; set; }  //Aktivasyon tamamlanma tarihi. | Eski alan: SirketTanim.AktivasyonTarihi
        public string Title { get; set; }  //Sirket unvani. | Eski alan: SirketTanim.Unvan
        public string Email { get; set; }  //Sirket e-posta adresi. | Eski alan: SirketTanim.EMail
        public string WebsiteUrl { get; set; }  //Web sitesi URL. | Eski alan: SirketTanim.WebSayfasi
        public string Address { get; set; }  //Acik adres. | Eski alan: SirketTanim.Adres
        public string District { get; set; }  //Ilce adi. | Eski alan: SirketTanim.Ilce
        public string MapUrl { get; set; }  //Harita/konum URL. | Eski alan: SirketTanim.MapUrl
        public string TaxOffice { get; set; }  //Vergi dairesi. | Eski alan: SirketTanim.VergiDairesi
        public string TaxNumber { get; set; }  //Vergi kimlik numarasi (VKN). | Eski alan: SirketTanim.VergiNumarasi
        public string Phone1 { get; set; }  //Birincil telefon. | Eski alan: SirketTanim.Telefon1
        public string Phone2 { get; set; }  //Ikincil telefon. | Eski alan: SirketTanim.Telefon2
        public string Fax { get; set; }  //Faks numarasi. | Eski alan: SirketTanim.Faks
        public string AuthorizedPerson { get; set; }  //Yetkili kisi. | Eski alan: SirketTanim.Yetkili
        public string DefaultCurrencyCode { get; set; }  //Ana para birimi kodu (TRY, USD vb.). | Eski alan: SirketTanim.StandartDovizKodu
        public string CurrencyCodes { get; set; }  //Merkez Bankasindan alinacak kur kodlari listesi. Isletme hangi kurlarla calisacagini belirler. | Eski alan: SirketTanim.Kurlar
        public string Parameters { get; set; }  //Genel isletme parametreleri. | Eski alan: SirketTanim.Parametreler
        public long? LegacyServerId { get; set; }  //Slave sunucu (FK -> ServerTanim.Id). | Eski alan: SirketTanim.ServerId
        public int WeightBarcodeLength { get; set; }  //Agirlik barkodu toplam uzunlugu. | Eski alan: SirketTanim.KgBarkodUzunlugu
        public int WeightBarcodeDecimalLength { get; set; }  //Agirlik barkodu ondalik hane sayisi. | Eski alan: SirketTanim.KgBarkodOndalikUzunluk
        public int? Integrator { get; set; }  //e-Belge entegratoru: 0=MukellefDegilim, 50=Logo, 100=NesBilgi. | Eski alan: SirketTanim.Entegrator
        public string IntegratorUserName { get; set; }  //Entegrator kullanici adi (sifreli). | Eski alan: SirketTanim.EntegratorUserName
        public string IntegratorPassword { get; set; }  //Entegrator sifresi (sifreli). | Eski alan: SirketTanim.EntegratorPassword
        public int? UserCount { get; set; }  //Izin verilen maksimum kullanici sayisi. | Eski alan: SirketTanim.KullaniciSayisi
        public string AboutUs { get; set; }  //E-ticaret hakkimizda metni. | Eski alan: SirketTanim.Hakkimizda
        public string ColorSizeLabel { get; set; }  //Varyant etiketi (varsayilan Beden). | Eski alan: SirketTanim.RenkBedenStr
        public string EcommerceStyle { get; set; }  //Birincil e-ticaret tema CSS dosyasi. | Eski alan: SirketTanim.ETicaretStyle
        public string EcommerceStylePrefix { get; set; }  //Birincil tema dosya oneki. | Eski alan: SirketTanim.ETicaretStylePrefix
        public string FavIconPrefix { get; set; }  //Birincil favicon oneki. | Eski alan: SirketTanim.FavIconPrefix
        public string EcommerceStyle1 { get; set; }  //Ikincil e-ticaret tema CSS dosyasi. | Eski alan: SirketTanim.ETicaretStyle1
        public string EcommerceStylePrefix1 { get; set; }  //Ikincil tema dosya oneki. | Eski alan: SirketTanim.ETicaretStylePrefix1
        public string FavIconPrefix1 { get; set; }  //Ikincil favicon oneki. | Eski alan: SirketTanim.FavIconPrefix1
        public decimal FreeShippingLimit { get; set; }  //Ucretsiz kargo limit tutari. | Eski alan: SirketTanim.KargoBedavaLimit
        public decimal? WeightServiceFee { get; set; }  //Kg bazli kargo hizmet bedeli. | Eski alan: SirketTanim.KgHizmetBedeli
        public decimal? DesiServiceFee { get; set; }  //Desi bazli kargo hizmet bedeli. | Eski alan: SirketTanim.DesiHizmetBedeli
        public decimal ShippingFee { get; set; }  //Sabit kargo ucreti. | Eski alan: SirketTanim.KargoBedeli
        public string ShippingLabel { get; set; }  //Kargo etiket sablonu. | Eski alan: SirketTanim.KargoEtiket
        public string FacebookUrl { get; set; }  //Facebook URL. | Eski alan: SirketTanim.FaceBookUrl
        public string InstagramUrl { get; set; }  //Instagram URL. | Eski alan: SirketTanim.InstagramUrl
        public string TwitterUrl { get; set; }  //Twitter/X URL. | Eski alan: SirketTanim.TwitterUrl
        public string PinterestUrl { get; set; }  //Pinterest URL. | Eski alan: SirketTanim.PinterestUrl
        public string YoutubeUrl { get; set; }  //YouTube URL. | Eski alan: SirketTanim.YoutubeUrl
        public bool? IsBankTransferActive { get; set; }  //Banka havalesi odeme aktif mi? | Eski alan: SirketTanim.BankaHavalesiAktif
        public bool? IsCashOnDeliveryActive { get; set; }  //Kapida odeme aktif mi? | Eski alan: SirketTanim.KapidaOdemeAktif
        public string EcommerceSiteName { get; set; }  //E-ticaret site basligi. | Eski alan: SirketTanim.ETicaretSiteAdi
        public string ShowcaseName { get; set; }  //Ana vitrin basligi. | Eski alan: SirketTanim.VitrinAdi
        public string ShowcaseLabel { get; set; }  //Vitrin etiket metni. | Eski alan: SirketTanim.VitrinEtiket
        public string GoogleAnalyticsId { get; set; }  //Google Analytics ID. | Eski alan: SirketTanim.GoogleAnalisticId
        public string FacebookPixelId { get; set; }  //Facebook Pixel ID. | Eski alan: SirketTanim.FacebookPixelId
        public string WhatsAppPhone { get; set; }  //WhatsApp iletisim numarasi. | Eski alan: SirketTanim.WhatsAppTelefon
        public bool IsSslActive { get; set; }  //HTTPS zorunlulugu aktif mi? | Eski alan: SirketTanim.SSLAktif
        public bool IsWideTopMenuActive { get; set; }  //Genis ust menu aktif mi? | Eski alan: SirketTanim.GenisUstMenuAktif
        public bool AllProductsMainMenuActive { get; set; }  //Tum urunler ana menude mi? | Eski alan: SirketTanim.TumUrunlerAnaMenuAktif
        public bool AllProductsSubMenuActive { get; set; }  //Tum urunler alt menude mi? | Eski alan: SirketTanim.TumUrunlerAltMenuAktif
        public int ProductImageWidth { get; set; }  //Urun resmi genisligi (px). | Eski alan: SirketTanim.UrunResimGenislik
        public int ProductImageHeight { get; set; }  //Urun resmi yuksekligi (px). | Eski alan: SirketTanim.UrunResimYukseklik
        public string SeoKeywords { get; set; }  //Meta keywords (SEO). | Eski alan: SirketTanim.SEOKelimeler
        public bool GenerateSitemap { get; set; }  //Otomatik sitemap olusturulsun mu? | Eski alan: SirketTanim.SiteHaritasiOlustur
        public bool HideCategoryText { get; set; }  //Kategori yazilari gizlensin mi? | Eski alan: SirketTanim.KategoriYaziGizle
        public bool ShowAllStores { get; set; }  //Tum depolar gosterilsin mi? | Eski alan: SirketTanim.TumDepolariGoster
        public bool ShowAllCashRegisters { get; set; }  //Tum kasalar gosterilsin mi? | Eski alan: SirketTanim.TumKasalariGoster
        public string TrendyolSellerId { get; set; }  //Trendyol satici ID. | Eski alan: SirketTanim.TrendyolSaticiId
        public string TrendyolApiKey { get; set; }  //Trendyol API key. | Eski alan: SirketTanim.TrendyolApiKey
        public string TrendyolApiSecret { get; set; }  //Trendyol API secret. | Eski alan: SirketTanim.TrendyolAPISecret
        public bool ShowColorSizeOnProductScreen { get; set; }  //Urun detayda renk/beden secimi gosterilsin mi? | Eski alan: SirketTanim.UrunEkraniRenkBeden
        public decimal FirstPurchaseDiscountPercent { get; set; }  //Ilk alisveris iskonto yuzdesi. | Eski alan: SirketTanim.IlkAlisverisdeIskontoYuzdesi
        public bool? UseOwnMailSettings { get; set; }  //Isletme kendi SMTP sunucusunu kullansin mi? | Eski alan: SirketTanim.KendiMailimiKullan
        public string MailAddress { get; set; }  //Gonderen e-posta adresi. | Eski alan: SirketTanim.MailAddress
        public string MailDisplayName { get; set; }  //Gonderen gorunen adi. | Eski alan: SirketTanim.MailDisplayName
        public string MailUserName { get; set; }  //SMTP kullanici adi. | Eski alan: SirketTanim.MailUserName
        public string MailPassword { get; set; }  //SMTP sifresi. | Eski alan: SirketTanim.MailPassword
        public string MailHost { get; set; }  //SMTP sunucu adresi. | Eski alan: SirketTanim.MailHost
        public int? MailPort { get; set; }  //SMTP port. | Eski alan: SirketTanim.MailPort
        public bool? IsDynamicExchangeRate { get; set; }  //Merkez kurlari otomatik guncellensin mi? | Eski alan: SirketTanim.DinamikDovizKuru
        public int LogoHeight { get; set; }  //Site logo yuksekligi (px). | Eski alan: SirketTanim.LogoYukseklik
        public string MersisNumber { get; set; }  //MERSIS numarasi. | Eski alan: SirketTanim.MersisNo
        public string TradeRegistryNumber { get; set; }  //Ticaret sicil numarasi. | Eski alan: SirketTanim.TicaretSicilNo
        public bool ImportExternalData { get; set; }  //Harici sistemden veri cekme aktif mi? | Eski alan: SirketTanim.DisaridanVeriAl
        public string ExternalSystemName { get; set; }  //Harici sistem adi. | Eski alan: SirketTanim.DisSistemAdi
        public string ExternalSystemIp { get; set; }  //Harici sistem IP/adres. | Eski alan: SirketTanim.DisSistemIP
        public string ExternalSystemDb { get; set; }  //Harici sistem veritabani. | Eski alan: SirketTanim.DisSistemDB
        public string ExternalSystemUser { get; set; }  //Harici sistem DB kullanicisi. | Eski alan: SirketTanim.DisSistemUser
        public string ExternalSystemPassword { get; set; }  //Harici sistem DB sifresi. | Eski alan: SirketTanim.DisSistemParola
        public string IntegratorEInvoiceTemplate { get; set; }  //e-Fatura tasarim/XSLT sablonu. | Eski alan: SirketTanim.EntegratorEfaturaTasarim
        public string IntegratorEArchiveTemplate { get; set; }  //e-Arsiv tasarim/XSLT sablonu. | Eski alan: SirketTanim.EntegratorEarsivTasarim
        public string SmsPhoneNumber { get; set; }  //SMS gonderim telefonu. | Eski alan: SirketTanim.SMSTelNo
        public string SmsTitle { get; set; }  //SMS baslik (originator). | Eski alan: SirketTanim.SmsBaslik
        public string SmsUserName { get; set; }  //SMS servis kullanici adi. | Eski alan: SirketTanim.SmsUserName
        public string SmsPassword { get; set; }  //SMS servis sifresi. | Eski alan: SirketTanim.SmsPassword
        public string AppointmentFirstSms { get; set; }  //Randevu olusturma SMS sablonu. | Eski alan: SirketTanim.RandevuIlkSMS
        public string AppointmentChangeSms { get; set; }  //Randevu degisiklik SMS sablonu. | Eski alan: SirketTanim.RandevuDegisiklikSMS
        public string AppointmentCancelForceSms { get; set; }  //Mucbir sebeple iptal SMS sablonu. | Eski alan: SirketTanim.RandevuIptalMucbirSMS
        public string AppointmentCancelPatientSms { get; set; }  //Hasta istegiyle iptal SMS sablonu. | Eski alan: SirketTanim.RandevuIptaHastaninIstegiSMS
        public string AppCode { get; set; }  //Uygulama kodu (web.config AppCode). | Eski alan: SirketTanim.AppCode
        public DateTime? InsertDateTime { get; set; }  //Kayit olusturma tarihi. | Eski alan: SirketTanim.InsertDateTime
        public long? UpdateUser { get; set; }  //Son guncelleyen kullanici. | Eski alan: SirketTanim.UpdateUser
        public DateTime? UpdateDateTime { get; set; }  //Son guncelleme tarihi. | Eski alan: SirketTanim.UpdateDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici (soft delete). | Eski alan: SirketTanim.DeleteUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi (soft delete). | Eski alan: SirketTanim.DeleteDateTime
        public virtual ICollection<TradeDocumentTemp> TradeDocumentTemp { get; set; } = new List<TradeDocumentTemp>();
        public virtual ICollection<AccountTransaction> AccountTransaction { get; set; } = new List<AccountTransaction>();
        public virtual ICollection<ReportDef> ReportDef { get; set; } = new List<ReportDef>();
        public virtual ICollection<CashRegister> CashRegister { get; set; } = new List<CashRegister>();
        public virtual ICollection<CashRegisterPersonal> CashRegisterPersonal { get; set; } = new List<CashRegisterPersonal>();
        public virtual ICollection<CashTransaction> CashTransaction { get; set; } = new List<CashTransaction>();
        public virtual ICollection<AppExceptionLog> AppExceptionLog { get; set; } = new List<AppExceptionLog>();
        public virtual ICollection<CheckNote> CheckNote { get; set; } = new List<CheckNote>();
        public virtual ICollection<CheckNoteTransaction> CheckNoteTransaction { get; set; } = new List<CheckNoteTransaction>();
        public virtual ICollection<MailLog> MailLog { get; set; } = new List<MailLog>();
        public virtual ICollection<Currency> Currency { get; set; } = new List<Currency>();
        public virtual ICollection<GroupDef> GroupDef { get; set; } = new List<GroupDef>();
        public virtual ICollection<CurrencyRate> CurrencyRate { get; set; } = new List<CurrencyRate>();
        public virtual ICollection<Brand> Brand { get; set; } = new List<Brand>();
        public virtual ICollection<GroupDefCountry> GroupDefCountry { get; set; } = new List<GroupDefCountry>();
        public virtual ICollection<ProcessTypePersonal> ProcessTypePersonal { get; set; } = new List<ProcessTypePersonal>();
        public virtual ICollection<Counter> Counter { get; set; } = new List<Counter>();
        public virtual ICollection<PersonalGroup> PersonalGroup { get; set; } = new List<PersonalGroup>();
        public virtual ICollection<CounterReference> CounterReference { get; set; } = new List<CounterReference>();
        public virtual ICollection<PersonalGroupDef> PersonalGroupDef { get; set; } = new List<PersonalGroupDef>();
        public virtual ICollection<Store> Store { get; set; } = new List<Store>();
        public virtual ICollection<EmailTemplate> EmailTemplate { get; set; } = new List<EmailTemplate>();
        public virtual ICollection<PersonalCompany> PersonalCompany { get; set; } = new List<PersonalCompany>();
        public virtual ICollection<StorePersonal> StorePersonal { get; set; } = new List<StorePersonal>();
        public virtual ICollection<License> License { get; set; } = new List<License>();
        public virtual ICollection<Unit> Unit { get; set; } = new List<Unit>();
        public virtual ICollection<TradeDocument> TradeDocument { get; set; } = new List<TradeDocument>();
        public virtual ICollection<Token> Token { get; set; } = new List<Token>();
        public virtual ICollection<Account> Account { get; set; } = new List<Account>();
        public virtual ICollection<TradeDocumentCurrency> TradeDocumentCurrency { get; set; } = new List<TradeDocumentCurrency>();
        public virtual ICollection<AccountAddress> AccountAddress { get; set; } = new List<AccountAddress>();
        public virtual ICollection<TradeDocumentLine> TradeDocumentLine { get; set; } = new List<TradeDocumentLine>();
        public virtual ICollection<AccountDocument> AccountDocument { get; set; } = new List<AccountDocument>();
        public virtual ICollection<TradeDocumentLineTemp> TradeDocumentLineTemp { get; set; } = new List<TradeDocumentLineTemp>();
        public virtual ICollection<TranslationDef> TranslationDef { get; set; } = new List<TranslationDef>();
    }
}
