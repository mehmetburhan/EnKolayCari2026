using System;
using EnKolayCari2026.Domain.Model.Common;
using EnKolayCari2026.Domain.Model.Dbo;
using EnKolayCari2026.Domain.Model.Finance;
using EnKolayCari2026.Domain.Model.HangFire;
using EnKolayCari2026.Domain.Model.Inventory;
using EnKolayCari2026.Domain.Model.Report;

namespace EnKolayCari2026.Domain.Model.Trade
{
    public class TradeDocument  //Eski tablo: Fatura (TICARI_SLAVE1). Yeni şema: trade.TradeDocument
    {
        public long Id { get; set; }  //Birincil anahtar. | Eski alan: Fatura.Id
        public Guid GId { get; set; }  //Global benzersiz kimlik (GUID). | Eski alan: Fatura.GId
        public long CompanyId { get; set; }  //Bagli sirket. | Eski alan: Fatura.SirketTanimId
        public long StoreId { get; set; }  //Ana depo (FK -> DepoTanim.Id). | Eski alan: Fatura.DepoTanimId
        public long? SecondaryStoreId { get; set; }  //Ikincil/hedef depo. | Eski alan: Fatura.DepoTanim1Id
        public long AccountId { get; set; }  //Bagli cari (FK -> CariTanim.Id). | Eski alan: Fatura.CariTanimId
        public int DocumentType { get; set; }  //Tanimsiz=0, SatisFaturasi=100, SatisIrsaliye=101, AlinanSiparis=103, VerilenTeklif=104, VerilenServis=105, DepoCikis=106, AlisIadeFaturasi=107, SayimGiris=108, AlisFaturasi=200, AlisIrsaliye=201, VerilenSiparis=203, AlinanTeklif=204, DepoGiris=206, SatisIadeFaturasi=207, SayimCikis=208 | Eski alan: Fatura.HareketTipi
        public string TradeDocumentNo { get; set; }  //Belge/fatura numarasi. | Eski alan: Fatura.FaturaNo
        public DateTime TransactionDate { get; set; }  //Belge tarihi. | Eski alan: Fatura.Tarih
        public DateTime? TransactionTime { get; set; }  //Belge saati. | Eski alan: Fatura.Saat
        public DateTime? DueDate { get; set; }  //Vade tarihi. | Eski alan: Fatura.VadeTarihi
        public string CurrencyCode { get; set; }  //Para birimi. | Eski alan: Fatura.DovizKodu
        public int? OrderStatus { get; set; }  //E-ticaret siparis durumu. | Eski alan: Fatura.SiparisDurumu
        public string OrderShippingSlipNumber { get; set; }  //Kargo takip/fis numarasi. | Eski alan: Fatura.SiparisKargoFisNo
        public long? ShippingCompanyDefId { get; set; }  //Kargo firmasi ID. | Eski alan: Fatura.KargoFirmaTanimId
        public string BillingFullNameOrTitle { get; set; }  //Fatura teslim alici. | Eski alan: Fatura.FaturaTeslimAdSoyadUnvan
        public string BillingEmail { get; set; }  //Fatura teslim e-posta. | Eski alan: Fatura.FaturaTeslimEMail
        public string BillingPhone { get; set; }  //Fatura teslim telefon. | Eski alan: Fatura.FaturaTeslimTelefon
        public string Address { get; set; }  //Fatura adresi. | Eski alan: Fatura.Adres
        public string City { get; set; }  //Fatura ili. | Eski alan: Fatura.Il
        public string District { get; set; }  //Fatura ilcesi. | Eski alan: Fatura.Ilce
        public string PostalCode { get; set; }  //Fatura posta kodu. | Eski alan: Fatura.PostaKodu
        public bool DeliverToDifferentAddress { get; set; }  //Farkli adrese teslim mi? | Eski alan: Fatura.FarkliAdreseTeslim
        public string DeliveryFullNameOrTitle { get; set; }  //Teslimat alici. | Eski alan: Fatura.TeslimatAdSoyadUnvan
        public string DeliveryEmail { get; set; }  //Teslimat e-posta. | Eski alan: Fatura.TeslimatEMail
        public string DeliveryPhone { get; set; }  //Teslimat telefon. | Eski alan: Fatura.TeslimatTelefon
        public string DeliveryAddress { get; set; }  //Teslimat adresi. | Eski alan: Fatura.TeslimatAdres
        public string DeliveryCity { get; set; }  //Teslimat ili. | Eski alan: Fatura.TeslimatIl
        public string DeliveryDistrict { get; set; }  //Teslimat ilcesi. | Eski alan: Fatura.TeslimatIlce
        public string DeliveryPostalCode { get; set; }  //Teslimat posta kodu. | Eski alan: Fatura.TeslimatPostaKodu
        public string TaxOffice { get; set; }  //Vergi dairesi. | Eski alan: Fatura.VergiDairesi
        public string TaxNumber { get; set; }  //Vergi numarasi. | Eski alan: Fatura.VergiNumarasi
        public long? RelatedTradeDocumentId { get; set; }  //Teklif->Siparis->Irsaliye->Fatura zincirinde sonraki belge ID. | Eski alan: Fatura.BagliFaturaId
        public string MedicalServiceProductBrandModel { get; set; }  //Servis: urun marka/model. | Eski alan: Fatura.ServisUrunMarkaModel
        public string ServiceDeviceSerialNo { get; set; }  //Servis: cihaz seri no. | Eski alan: Fatura.ServisCihazSeriNo
        public string MedicalServiceSellerCompany { get; set; }  //Servis: satici firma. | Eski alan: Fatura.ServisSaticiFirma
        public string ServiceAccessories { get; set; }  //Servis: aksesuarlar. | Eski alan: Fatura.ServisAksesuar
        public string ServiceDeviceDescription { get; set; }  //Servis: cihaz aciklamasi. | Eski alan: Fatura.ServisCihazAciklama
        public string ServiceCustomerNote { get; set; }  //Servis: musteri notu. | Eski alan: Fatura.ServisMusteriNotu
        public string ServicePersonalNote { get; set; }  //Servis: personel notu. | Eski alan: Fatura.ServisPersonelNotu
        public bool? ServiceWarrantyInfo { get; set; }  //Servis: garanti bilgisi. | Eski alan: Fatura.ServisGarantiBilgisi
        public int? MedicalServiceStatus { get; set; }  //YeniSiparis=0, Hazirlaniyor=100, KargoyaVerildi=200, TeslimEdildi=300, Iade=400, Iptal=500 | Eski alan: Fatura.ServisDurumu
        public DateTime? ServiceDeliveryDate { get; set; }  //Servis teslim tarihi. | Eski alan: Fatura.ServisTeslimTarihi
        public string ServiceDeliveryRecipient { get; set; }  //Servisi teslim alan kisi. | Eski alan: Fatura.ServisTeslimAlanKisi
        public int? QuoteStatus { get; set; }  //-100=TeklifIptal, 100=YeniTeklif, 200=TeklifKabulEdildi, 300=SozlesmeImzalandi, 400=KabulEdilmedi | Eski alan: Fatura.TeklifDurum
        public string Label { get; set; }  //Etiket. | Eski alan: Fatura.Etiket
        public bool IsDocumentClosed { get; set; }  //Belge kapatildi mi (stok/cari kilit)? | Eski alan: Fatura.BelgeKapali
        public DateTime? ED_LastProcessDate { get; set; }  //e-Belge son islem tarihi. | Eski alan: Fatura.ED_SonIslemTarihi
        public int? ED_Code { get; set; }  //e-Belge islem kodu. | Eski alan: Fatura.ED_Code
        public string ED_Description { get; set; }  //e-Belge islem aciklamasi. | Eski alan: Fatura.ED_Description
        public string ED_DetailDescription { get; set; }  //e-Belge detay aciklamasi. | Eski alan: Fatura.ED_DetailDescription
        public int? RecordSource { get; set; }  //0=Normal, 1=ETicaret | Eski alan: Fatura.KayitYeri
        public int? EcommercePaymentMethod { get; set; }  //1=KrediKarti, 2=BankayaHavale, 3=Kapida | Eski alan: Fatura.ETicaretOdemeSekli
        public string EcommercePaymentBank { get; set; }  //Odeme bankasi/kanal adi. | Eski alan: Fatura.ETicaretOdemeBanka
        public string EcommercePaymentId { get; set; }  //Odeme referans/islem ID. | Eski alan: Fatura.ETicaretOdemeId
        public string IntegrationId { get; set; }  //Harici entegrasyon belge ID. | Eski alan: Fatura.EntegrasyonId
        public string IntegrationName { get; set; }  //Harici entegrasyon adi. | Eski alan: Fatura.EntegrasyonAdi
        public int LineCount { get; set; }  //Toplam kalem sayisi. | Eski alan: Fatura.KalemSayisi
        public decimal TotalVatAmount { get; set; }  //Toplam KDV tutari. | Eski alan: Fatura.ToplamKdvTutar
        public decimal TotalAmount { get; set; }  //Toplam belge tutari. | Eski alan: Fatura.ToplamTutar
        public DateTime InsertDateTime { get; set; }  //Olusturma tarihi. | Eski alan: Fatura.InsertDateTime
        public long InsertUser { get; set; }  //Olusturan kullanici. | Eski alan: Fatura.InsertUser
        public DateTime? UpdateDateTime { get; set; }  //Guncelleme tarihi. | Eski alan: Fatura.UpdateDateTime
        public long? UpdateUser { get; set; }  //Guncelleyen kullanici. | Eski alan: Fatura.UpdateUser
        public DateTime? DeleteDateTime { get; set; }  //Silme tarihi. | Eski alan: Fatura.DeleteDateTime
        public long? DeleteUser { get; set; }  //Silen kullanici. | Eski alan: Fatura.DeleteUser
        public DateTime? RecordDateTime { get; set; }  //Kayit zaman damgasi. | Eski alan: Fatura.RecDateTime
        public bool IsDocumentCancelled { get; set; }  //Belge iptal edildi mi? | Eski alan: Fatura.BelgeIptal
        public int? ElectronicDocumentType { get; set; }  //1=EArsiv, 2=EFatura, 3=EIrsaliye | Eski alan: Fatura.ElektronikBelgeTipi
        public string ElectronicDocumentNo { get; set; }  //e-Belge numarasi. | Eski alan: Fatura.ElektronikBelgeNo
        public DateTime? ElectronicDocumentSentDate { get; set; }  //e-Belge gonderim tarihi. | Eski alan: Fatura.ElektronikBelgeGonderimTarihi
        public string ElectronicDocumentErrors { get; set; }  //e-Belge gonderim hata mesajlari. | Eski alan: Fatura.ElektronikBelgeHatalari
        public int? ElectronicDocumentSendStatus { get; set; }  //null=Gonderilmemis, -1=Hatali, 1=Basarili | Eski alan: Fatura.ElektronikBelgeGonderimDurumu

        public virtual Company Company { get; set; }
        public virtual Store Store { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<AccountTransaction> AccountTransaction { get; set; } = new List<AccountTransaction>();
        public virtual ICollection<TradeDocumentCurrency> TradeDocumentCurrency { get; set; } = new List<TradeDocumentCurrency>();
        public virtual ICollection<TradeDocumentLine> TradeDocumentLine { get; set; } = new List<TradeDocumentLine>();
    }
}
