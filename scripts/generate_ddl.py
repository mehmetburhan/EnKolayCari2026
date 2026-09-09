#!/usr/bin/env python3
"""Generate EnKolayCari unified database DDL from legacy table docs."""

import json
import re
import os
from pathlib import Path
from collections import OrderedDict

ROOT = Path(__file__).resolve().parents[1]
LEGACY_DOCS = Path(__file__).resolve().parents[2] / "EnKolayCari" / "docs" / "database"
MAPPINGS = ROOT / "database" / "mappings" / "tables.json"
OUTPUT_SQL = ROOT / "database" / "EnKolayCari_CreateDatabase.sql"
OUTPUT_MAPPING_MD = ROOT / "docs" / "TABLE-MAPPING.md"
DB_NAME = "EKCN2026"

# Common column name translations (old TR -> new EN)
COLUMN_MAP = {
    "Id": "Id",
    "GId": "GId",
    "Aktif": "Stat",
    "Kod": "Code",
    "Unvan": "Title",
    "Ad": "FirstName",
    "Soyad": "LastName",
    "AdSoyadUnvan": "FullNameOrTitle",
    "EMail": "Email",
    "Parola": "Password",
    "YazismaEMail": "CorrespondenceEmail",
    "AktivasyonYapildi": "IsActivationCompleted",
    "AktivasyonTarihi": "ActivationDate",
    "WebSayfasi": "WebsiteUrl",
    "Adres": "Address",
    "Il": "City",
    "Ilce": "District",
    "MapUrl": "MapUrl",
    "VergiDairesi": "TaxOffice",
    "VergiNumarasi": "TaxNumber",
    "Telefon1": "Phone1",
    "Telefon2": "Phone2",
    "Telefon": "Phone",
    "Faks": "Fax",
    "Yetkili": "AuthorizedPerson",
    "StandartDovizKodu": "DefaultCurrencyCode",
    "Kurlar": "CurrencyCodes",
    "Parametreler": "Parameters",
    "SirketTanimId": "CompanyId",
    "ServerId": "LegacyServerId",
    "InsertUser": "InsertUser",
    "RecDateTime": "RecordDateTime",
    "InsertDateTime": "InsertDateTime",
    "UpdateDateTime": "UpdateDateTime",
    "UpdateUser": "UpdateUser",
    "DeleteDateTime": "DeleteDateTime",
    "DeleteUser": "DeleteUser",
    "IsDelete": "IsDelete",
    "CariTipi": "AccountType",
    "CariKodu": "AccountCode",
    "Aciklama": "Description",
    "Tarih": "TransactionDate",
    "Tutar": "Amount",
    "Borc": "DebitAmount",
    "Alacak": "CreditAmount",
    "Bakiye": "Balance",
    "PersonelTanimId": "PersonalId",
    "PersonelId": "PersonalId",
    "PersonelGId": "PersonalGId",
    "CariTanimId": "AccountId",
    "FaturaId": "TradeDocumentId",
    "FaturaHareketId": "TradeDocumentLineId",
    "UrunTanimId": "ProductId",
    "DepoTanimId": "StoreId",
    "DepoId": "StoreId",
    "DepoKodu": "StoreCode",
    "KasaTanimId": "CashRegisterId",
    "DovizTanimId": "CurrencyId",
    "MarkaTanimId": "BrandId",
    "MarkaKodu": "BrandCode",
    "KategoriTanimId": "CategoryId",
    "BirimTanimId": "UnitId",
    "FiyatGrupTanimId": "PriceGroupId",
    "ModulId": "ModulId",
    "LisansId": "LicenseId",
    "LisansTipiId": "LicenseTypeId",
    "LisansTipi": "LicenseType",
    "SayfaTanimId": "PageDefId",
    "RoleId": "RoleId",
    "UserId": "UserId",
    "Token": "TokenValue",
    "ExpireDate": "ExpireDate",
    "CreatedDate": "CreatedDate",
    "CreatedUser": "CreatedUser",
    "ModifedDate": "ModifedDate",
    "ModifedUser": "ModifedUser",
    "DeletedDate": "DeletedDate",
    "DeletedUser": "DeletedUser",
    # --- Account / finance ---
    "EMukellefAktif": "IsETaxpayerActive",
    "IskontoOrani": "DiscountRate",
    "IskontoYuzde": "DiscountPercent",
    "Iskonto1Yuzde": "Discount1Percent",
    "KayitYeri": "RecordSource",
    "BankaHesapSahibi": "BankAccountHolder",
    "BankaSubeKodu": "BankBranchCode",
    "BankaSubesi": "BankBranch",
    "BankaTakip": "BankTracking",
    "HesapNo": "AccountNumber",
    "PostaKodu": "PostalCode",
    "EntegrasyonAdi": "IntegrationName",
    "EntegrasyonId": "IntegrationId",
    "Etiket": "Label",
    "ETicaretIBANGoster": "ShowIbanOnEcommerce",
    "EBulten": "NewsletterOptIn",
    "SmsAlmakIstiyorum": "SmsOptIn",
    "UyelikSozlesmesiniOkudum": "MembershipAgreementAccepted",
    "KvkkOkudum": "KvkkAccepted",
    "TCKimlikNo": "NationalId",
    "Cinsiyet": "Gender",
    "DogumTarihi": "BirthDate",
    "KargoBedava": "FreeShipping",
    "TrendyolId": "TrendyolId",
    "TrendyolAciklama": "TrendyolDescription",
    "VadeGun": "PaymentTermDays",
    "SosyalGuvence": "SocialSecurity",
    "EkBilgi": "AdditionalInfo",
    "KVKKOnayKodu": "KvkkApprovalCode",
    # --- Product / inventory ---
    "Barkod": "Barcode",
    "KgBarkod": "IsWeightBarcode",
    "KgBarkodUzunlugu": "WeightBarcodeLength",
    "KgBarkodOndalikUzunluk": "WeightBarcodeDecimalLength",
    "SatisFiyati": "SalePrice",
    "SatisFiyatAktif": "IsSalePriceActive",
    "OncekiSatisFiyati": "PreviousSalePrice",
    "TaksitliSatisFiyati": "InstallmentSalePrice",
    "AlisFiyati": "PurchasePrice",
    "BirimFiyati": "UnitPrice",
    "BirimFiyatiKdvDahil": "UnitPriceVatIncluded",
    "AlisKdvOrani": "PurchaseVatRate",
    "AlisKdvDH": "PurchaseVatIncluded",
    "SatisKdvDH": "SaleVatIncluded",
    "KdvOrani": "VatRate",
    "AlisDovizCode": "PurchaseCurrencyCode",
    "SatisDovizCode": "SaleCurrencyCode",
    "AnaUrun": "IsMainProduct",
    "AnaDepo": "IsMainStore",
    "Sira": "SortOrder",
    "SiradakiNo": "NextNumber",
    "Renk": "Color",
    "RenkId": "ColorId",
    "RenkKodu": "ColorCode",
    "RenkAciklama": "ColorDescription",
    "Beden": "Size",
    "RenkBeden": "ColorSize",
    "RenkBedenStr": "ColorSizeLabel",
    "UrunRenkPaletiId": "ProductColorPaletteId",
    "ProductRenkPaletiId": "ProductColorPaletteId",
    "UrunEkraniRenkBeden": "ShowColorSizeOnProductScreen",
    "ProductEkraniRenkBeden": "ShowColorSizeOnProductScreen",
    "HepsiBuradaUrunId": "HepsiBuradaProductId",
    "HepsiburadaAktif": "IsHepsiBuradaActive",
    "TrendyolAktif": "IsTrendyolActive",
    "TrendyolSaticiId": "TrendyolSellerId",
    "TrendyolApiKey": "TrendyolApiKey",
    "TrendyolAPISecret": "TrendyolApiSecret",
    "TransTypeTrendyol": "TrendyolSyncType",
    "InternetSatisAktif": "IsInternetSaleActive",
    "InternetKritikStokMiktari": "InternetCriticalStockQuantity",
    "KritikStokMiktari": "CriticalStockQuantity",
    "MinSatisMiktar": "MinSaleQuantity",
    "Desi": "Desi",
    "Genislik": "Width",
    "Yukseklik": "Height",
    "KalemSayisi": "LineCount",
    # --- Company / ecommerce settings ---
    "Entegrator": "Integrator",
    "EntegratorUserName": "IntegratorUserName",
    "EntegratorPassword": "IntegratorPassword",
    "EntegratorEfaturaTasarim": "IntegratorEInvoiceTemplate",
    "EntegratorEarsivTasarim": "IntegratorEArchiveTemplate",
    "BankaHavalesiAktif": "IsBankTransferActive",
    "KapidaOdemeAktif": "IsCashOnDeliveryActive",
    "KargoBedavaLimit": "FreeShippingLimit",
    "KargoBedeli": "ShippingFee",
    "KargoEtiket": "ShippingLabel",
    "KargoFirmaDefId": "ShippingCompanyDefId",
    "KgHizmetBedeli": "WeightServiceFee",
    "DesiHizmetBedeli": "DesiServiceFee",
    "ETicaretSiteAdi": "EcommerceSiteName",
    "ETicaretStyle": "EcommerceStyle",
    "ETicaretStyle1": "EcommerceStyle1",
    "ETicaretStylePrefix": "EcommerceStylePrefix",
    "ETicaretStylePrefix1": "EcommerceStylePrefix1",
    "ETicaretSatisAktif": "IsEcommerceSaleActive",
    "ETicaret": "IsEcommerce",
    "ETicaretOdemeBanka": "EcommercePaymentBank",
    "ETicaretOdemeId": "EcommercePaymentId",
    "ETicaretOdemeSekli": "EcommercePaymentMethod",
    "FavIconPrefix": "FavIconPrefix",
    "FavIconPrefix1": "FavIconPrefix1",
    "FaceBookUrl": "FacebookUrl",
    "InstagramUrl": "InstagramUrl",
    "TwitterUrl": "TwitterUrl",
    "PinterestUrl": "PinterestUrl",
    "YoutubeUrl": "YoutubeUrl",
    "VitrinAdi": "ShowcaseName",
    "VitrinEtiket": "ShowcaseLabel",
    "GoogleAnalisticId": "GoogleAnalyticsId",
    "FacebookPixelId": "FacebookPixelId",
    "WhatsAppTelefon": "WhatsAppPhone",
    "SSLAktif": "IsSslActive",
    "GenisUstMenuAktif": "IsWideTopMenuActive",
    "TumUrunlerAnaMenuAktif": "AllProductsMainMenuActive",
    "TumUrunlerAltMenuAktif": "AllProductsSubMenuActive",
    "TumUrunlerAnaMenuStat": "AllProductsMainMenuActive",
    "TumUrunlerAltMenuStat": "AllProductsSubMenuActive",
    "UrunResimGenislik": "ProductImageWidth",
    "UrunResimYukseklik": "ProductImageHeight",
    "ProductResimGenislik": "ProductImageWidth",
    "ProductResimYukseklik": "ProductImageHeight",
    "TransTypeHepsiburada": "HepsiBuradaSyncType",
    "TransTypeHepsiBurada": "HepsiBuradaSyncType",
    "TransTypeTrendyol": "TrendyolSyncType",
    "SEOKelimeler": "SeoKeywords",
    "SiteHaritasiOlustur": "GenerateSitemap",
    "KategoriYaziGizle": "HideCategoryText",
    "TumDepolariGoster": "ShowAllStores",
    "TumKasalariGoster": "ShowAllCashRegisters",
    "IlkAlisverisdeIskontoYuzdesi": "FirstPurchaseDiscountPercent",
    "KendiMailimiKullan": "UseOwnMailSettings",
    "MailAddress": "MailAddress",
    "MailDisplayName": "MailDisplayName",
    "MailUserName": "MailUserName",
    "MailPassword": "MailPassword",
    "MailHost": "MailHost",
    "MailPort": "MailPort",
    "DinamikDovizKuru": "IsDynamicExchangeRate",
    "LogoYukseklik": "LogoHeight",
    "MersisNo": "MersisNumber",
    "TicaretSicilNo": "TradeRegistryNumber",
    "DisaridanVeriAl": "ImportExternalData",
    "DisSistemAdi": "ExternalSystemName",
    "DisSistemIP": "ExternalSystemIp",
    "DisSistemDB": "ExternalSystemDb",
    "DisSistemUser": "ExternalSystemUser",
    "DisSistemParola": "ExternalSystemPassword",
    "SMSTelNo": "SmsPhoneNumber",
    "SmsBaslik": "SmsTitle",
    "SmsUserName": "SmsUserName",
    "SmsPassword": "SmsPassword",
    "RandevuIlkSMS": "AppointmentFirstSms",
    "RandevuDegisiklikSMS": "AppointmentChangeSms",
    "RandevuIptalMucbirSMS": "AppointmentCancelForceSms",
    "RandevuIptaHastaninIstegiSMS": "AppointmentCancelPatientSms",
    "RandevuAktif": "IsAppointmentActive",
    "RandevuAcik": "IsAppointmentOpen",
    "Hakkimizda": "AboutUs",
    "KullaniciSayisi": "UserCount",
    "Prefix": "Prefix",
    "Deger": "Value",
    # --- Trade document ---
    "BaslangicTarihi": "StartDate",
    "BitisTarihi": "EndDate",
    "VadeTarihi": "DueDate",
    "BelgeIptal": "IsDocumentCancelled",
    "BelgeIlkSahibiUnvan": "DocumentOriginalOwnerTitle",
    "ElektronikBelgeTipi": "ElectronicDocumentType",
    "ElektronikBelgeGonderimTarihi": "ElectronicDocumentSentDate",
    "EArsivEFatura": "EArchiveEInvoice",
    "FarkliAdreseTeslim": "DeliverToDifferentAddress",
    "TeslimatAdSoyadUnvan": "DeliveryFullNameOrTitle",
    "TeslimatAdres": "DeliveryAddress",
    "TeslimatIlce": "DeliveryDistrict",
    "TeslimatPostaKodu": "DeliveryPostalCode",
    "TeslimatTelefon": "DeliveryPhone",
    "TeslimatEMail": "DeliveryEmail",
    "TradeDocumentTeslimTelefon": "TradeDocumentDeliveryPhone",
    "TradeDocumentTeslimEMail": "TradeDocumentDeliveryEmail",
    "SiparisKargoFisNo": "OrderShippingSlipNumber",
    "OdemeTakip": "PaymentTracking",
    "OnayTipi": "ApprovalType",
    "BagliCariHareketId": "RelatedAccountTransactionId",
    "BagliFaturaId": "RelatedTradeDocumentId",
    "CikisFaturaId": "OutboundTradeDocumentId",
    "GirisFaturaId": "InboundTradeDocumentId",
    "AitOlduguBanka": "OwningBank",
    "AdresBaslik": "AddressTitle",
    "KimdeCariDefId": "HolderAccountDefId",
    "SayimTarihi": "StockCountDate",
    "SayimAciklama": "StockCountDescription",
    "RaporTipi": "ReportType",
    "ServisCihazAciklama": "ServiceDeviceDescription",
    "ServisPersonelNotu": "ServicePersonalNote",
    "ServisTeslimTarihi": "ServiceDeliveryDate",
    "MedicalServiceUrunMarkaModel": "MedicalServiceProductBrandModel",
    "ServisSaticiFirma": "MedicalServiceSellerCompany",
    "MedicalServiceSaticiFirma": "MedicalServiceSellerCompany",
    "UseEMailForLogin": "UseEmailForLogin",
}

# Longest-first token replacements inside compound names
TOKEN_REPLACEMENTS = [
    ("EMukellef", "ETaxpayer"),
    ("Iskonto", "Discount"),
    ("KayitYeri", "RecordSource"),
    ("RenkBeden", "ColorSize"),
    ("TrendyolSatici", "TrendyolSeller"),
    ("SaticiFirma", "SellerCompany"),
    ("Satici", "Seller"),
    ("HepsiBuradaUrun", "HepsiBuradaProduct"),
    ("Hepsiburada", "HepsiBurada"),
    ("Alisveris", "Purchase"),
    ("AlisFiyat", "PurchasePrice"),
    ("SatisFiyat", "SalePrice"),
    ("Onceki", "Previous"),
    ("Taksitli", "Installment"),
    ("Barkod", "Barcode"),
    ("KargoBedava", "FreeShipping"),
    ("Kargo", "Shipping"),
    ("KapidaOdeme", "CashOnDelivery"),
    ("BankaHavalesi", "BankTransfer"),
    ("Entegrator", "Integrator"),
    ("Entegrasyon", "Integration"),
    ("ETicaret", "Ecommerce"),
    ("Doviz", "Currency"),
    ("Kdv", "Vat"),
    ("Satis", "Sale"),
    ("Alis", "Purchase"),
    ("Renk", "Color"),
    ("Beden", "Size"),
    ("Depo", "Store"),
    ("Personel", "Personal"),
    ("Fatura", "TradeDocument"),
    ("Cari", "Account"),
    ("Urun", "Product"),
    ("Marka", "Brand"),
    ("Adres", "Address"),
    ("Telefon", "Phone"),
    ("Aciklama", "Description"),
    ("Tarihi", "Date"),
    ("Tarih", "Date"),
    ("Orani", "Rate"),
    ("Oran", "Rate"),
    ("Yuzdesi", "Percent"),
    ("Yuzde", "Percent"),
    ("Sayisi", "Count"),
    ("Miktari", "Quantity"),
    ("Miktar", "Quantity"),
    ("Fiyati", "Price"),
    ("Fiyat", "Price"),
    ("Aktif", "Active"),
    ("Baslik", "Title"),
    ("Unvan", "Title"),
    ("Parola", "Password"),
    ("Kullanici", "User"),
    ("Sira", "SortOrder"),
]

# Suffix replacements for auto-translation
SUFFIX_RULES = [
    ("TanimId", "DefId"),
    ("Tanim", "Def"),
    ("Hareket", "Transaction"),
    ("Kodu", "Code"),
    ("Adi", "Name"),
    ("Tarihi", "Date"),
    ("Tarih", "Date"),
    ("Sayisi", "Count"),
    ("Orani", "Rate"),
    ("Oran", "Rate"),
    ("Yuzdesi", "Percent"),
    ("Yuzde", "Percent"),
    ("Miktari", "Quantity"),
    ("Miktar", "Quantity"),
    ("Fiyati", "Price"),
    ("Fiyat", "Price"),
    ("Durumu", "Status"),
    ("Durum", "Status"),
    ("Tipi", "Type"),
    ("Tip", "Type"),
    ("Numarasi", "Number"),
    ("Aciklama", "Description"),
    ("Unvan", "Title"),
    ("Aktif", "Active"),
    ("Uzunlugu", "Length"),
    ("Uzunluk", "Length"),
    ("Yukseklik", "Height"),
    ("Genislik", "Width"),
    ("Bedeli", "Fee"),
    ("Limit", "Limit"),
]

# FK table name hints for *Id columns
FK_TABLE_HINTS = {
    "CompanyId": "common.Company",
    "PersonalId": "common.Personal",
    "AccountId": "finance.Account",
    "ProductId": "inventory.Product",
    "TradeDocumentId": "trade.TradeDocument",
    "TradeDocumentLineId": "trade.TradeDocumentLine",
    "StoreId": "inventory.Store",
    "CashRegisterId": "finance.CashRegister",
    "CurrencyId": "finance.Currency",
    "BrandId": "inventory.Brand",
    "UnitId": "inventory.Unit",
    "LicenseId": "common.License",
    "LicenseTypeId": "common.LicenseType",
    "PageDefId": "common.PageDef",
}

SCHEMAS = [
    "common", "dbo", "HangFire", "finance", "inventory",
    "trade", "report",
]

# Medical module not in scope for EnKolayCari
EXCLUDED_TABLES = {
    "ICD10", "HastaAnamnez", "HastaAnamnezKontrol", "HastaAnamnezPersonel",
    "Randevu", "Servis", "Survey", "SurveyAnswers", "QBank", "QBankOptions",
    "Conversation",
    # Location: replaced by AI architecture common.City (Level 1=Province, 2=District)
    "Iller", "Ilceler",
    "EMailKuyruk",
    # Files: replaced by AI architecture common.FileHeader + FileBlob
    "Dosyalar", "DosyalarBlob",
    "UserRoles",
    "ModulFiyat",
    "Modul",
    "sysdiagrams",
    "PersonelTanim",
    "Sms",
    # Deferred schemas: ecommerce
    "ETicaretAyarlar", "ETicaretCSS", "ETicaretEvent", "ETicaretGezilenUrunler",
    "ETicaretMenu", "ETicaretParametre", "ETicaretPopup", "ETicaretSlider",
    "ETicaretThema", "ETicaretThemaItem", "ETicaretUrunParametre",
    # Deferred schemas: integration
    "PazaryeriKategori", "PazaryeriMarka", "PazaryeriOzellik", "PazaryeriOzellikDeger",
    "PazaryeriLog", "EntegrasyonNetsis", "UrunPazaryeriOzellik",
    # Deferred schemas: content (Etiket* -> common.LabelDef/LabelPool)
    "Makale", "MakaleParagraf", "MakaleOneri", "SeoTanim", "BelgeTanim",
    "EtiketTanim", "EtiketHavuzu",
    # Deferred schemas: platform
    "Logs", "BinList", "TmpFace", "FieldTrToEn", "ServerTanim", "Server",
    # Deferred inventory (out of scope for now)
    "UrunReceteTanim", "UrunReceteHareket", "UrunYorum", "UrunFiyatGrup",
    "KategoriTanim", "KategoriDetay", "FiyatGrupTanim",
    "OzellikTanim", "OzellikDetay",
    # Deferred trade / report
    "EBelge", "EBelgeHareket",
    "Etkinlik", "EtkinlikKatilimci",
}

# Per-table column exclusions (legacy column names)
# Credit card / virtual POS fields removed from Company in the new system
EXCLUDED_COLUMNS = {
    "SirketTanim": {
        # Bank virtual POS credentials
        "GarantiAktif", "GarantiName", "GarantiPassword", "GarantiClientId",
        "YapiKrediAktif", "YapiKrediMid", "YapiKrediTid",
        "VakifBankAktif", "VakifBankKullanici", "VakifBankSifre",
        "VakifBankUyeNo", "VakifBankPosNo", "VakifBankXcip",
        "AkbankAktif", "AkbankName", "AkbankPassword", "AkbankClientId",
        "IsBankAktif", "IsBankName", "IsBankPassword", "IsBankClientId",
        "FinansBankAktif", "FinansBankName", "FinansBankPassword", "FinansBankClientId",
        "DenizBankAktif", "DenizBankName", "DenizBankPassword", "DenizBankClientId",
        "IyzicoAktif", "IyzicoAPIAnahtari", "IyzicoGuvenlikAnahtari",
        "ZiraatBankasiAktif", "ZiraatMerchantId", "ZiraatMerchantPassword",
        "ParamPosAktif", "ParamPosClientId", "ParamPosName", "ParamPosPassword", "ParamPosGuid",
        # Credit card payment / installment settings
        "KrediAktiOdemeAktif",
        "KartMaxCekimSayisi",
        "TaksitMinimumTutar",
        "KartVadeFarkiYasit",
    },
}


def load_table_mappings():
    with open(MAPPINGS, encoding="utf-8") as f:
        return json.load(f)


def parse_table_md(path: Path):
    text = path.read_text(encoding="utf-8")
    old_name = path.stem.replace("table-", "")
    db_match = re.search(r"\*\*Veritabani:\*\*\s*(\S+)", text)
    source_db = db_match.group(1) if db_match else "UNKNOWN"

    columns = []
    in_table = False
    for line in text.splitlines():
        if line.startswith("| `"):
            parts = [p.strip() for p in line.split("|")]
            if len(parts) >= 6:
                col_name = parts[1].strip("`")
                col_type = parts[2]
                nullable = parts[3]
                identity = parts[4]
                desc = parts[5] if len(parts) > 5 else ""
                columns.append({
                    "old_name": col_name,
                    "type": col_type,
                    "nullable": nullable.lower().startswith("evet"),
                    "identity": identity.lower().startswith("evet"),
                    "description": desc,
                })
    return old_name, source_db, columns


def camel_to_pascal(name: str) -> str:
    if not name:
        return name
    return name[0].upper() + name[1:]


def translate_column(old_name: str, table_old: str) -> str:
    if old_name in COLUMN_MAP:
        return COLUMN_MAP[old_name]

    result = old_name

    # Longest-first Turkish token → English (compound names)
    for tr, en in TOKEN_REPLACEMENTS:
        if tr in result:
            result = result.replace(tr, en)

    for tr, en in SUFFIX_RULES:
        if result.endswith(tr):
            result = result[: -len(tr)] + en
            break

    # Common prefix translations
    prefix_map = {
        "Sirket": "Company",
        "Personel": "Personal",
        "Cari": "Account",
        "Urun": "Product",
        "Fatura": "TradeDocument",
        "Kasa": "CashRegister",
        "Depo": "Store",
        "Doviz": "Currency",
        "Marka": "Brand",
        "Kategori": "Category",
        "Birim": "Unit",
        "FiyatGrup": "PriceGroup",
        "Lisans": "License",
        "Modul": "Module",
        "Pazaryeri": "Marketplace",
        "ETicaret": "Ecommerce",
        "Ecommerce": "Ecommerce",
        "Hasta": "Patient",
        "Makale": "Article",
        "Etiket": "Label",
        "Seo": "Seo",
        "Belge": "Document",
        "Dosya": "File",
        "EMail": "Email",
        "EBelge": "EDocument",
        "Entegrasyon": "Integration",
        "Integration": "Integration",
        "Ozellik": "Attribute",
        "Sayim": "StockCount",
        "Sayac": "Counter",
        "Servis": "MedicalService",
        "Randevu": "Appointment",
        "Etkinlik": "Event",
        "Rapor": "Report",
        "CekSenet": "CheckNote",
        "Merkez": "Central",
        "Iller": "Province",
        "Ilceler": "District",
        "Sms": "Sms",
        "Log": "Log",
        "Bin": "Bin",
        "Tmp": "Temp",
        "Field": "Field",
        "User": "User",
        "Role": "Role",
        "Server": "Server",
        "Token": "Token",
        "Sayfa": "Page",
        "Conversation": "Conversation",
        "Survey": "Survey",
        "QBank": "QuestionBank",
    }
    for tr, en in prefix_map.items():
        if result.startswith(tr):
            result = en + result[len(tr):]
            break

    return camel_to_pascal(result)


def sanitize_ident(name: str) -> str:
    """ASCII-safe SQL identifier; fixes corrupted Turkish docs (U+FFFD) and diacritics."""
    known = {
        "Hakk\ufffdmizda": "Hakkimizda",
        "Hakkmizda": "Hakkimizda",
        "Hakkımızda": "Hakkimizda",
        "Kay\ufffdtYeri": "KayitYeri",
        "KaytYeri": "KayitYeri",
        "KayıtYeri": "KayitYeri",
    }
    if name in known:
        return known[name]
    name = name.replace("\ufffd", "")
    tr_map = str.maketrans({
        "ç": "c", "Ç": "C", "ğ": "g", "Ğ": "G", "ı": "i", "İ": "I",
        "ö": "o", "Ö": "O", "ş": "s", "Ş": "S", "ü": "u", "Ü": "U",
    })
    name = name.translate(tr_map)
    name = re.sub(r"[^A-Za-z0-9_]", "", name)
    return name or "Col"


def sql_type(col_type: str, col_name: str) -> str:
    t = col_type.lower().strip()
    if t == "int":
        if col_name in ("Id",) or col_name.endswith("Id") or col_name.endswith("User"):
            return "bigint"
        return "int"
    if t == "bigint":
        return "bigint"
    if t == "bit":
        return "bit"
    if t == "uniqueidentifier":
        return "uniqueidentifier"
    if t == "datetime":
        return "datetime"
    if t == "datetime2":
        return "datetime2(7)"
    if t == "date":
        return "date"
    if t in ("text", "ntext"):
        return "nvarchar(max) COLLATE Turkish_CI_AI"
    if t == "image":
        return "varbinary(max)"
    if t == "float":
        return "float"
    if t.startswith("decimal"):
        return col_type
    if t.startswith("varbinary") or t.startswith("binary") or t in ("xml", "sql_variant"):
        return col_type  # no COLLATE
    if t.startswith("nvarchar"):
        return col_type + " COLLATE Turkish_CI_AI"
    if t.startswith("varchar"):
        return col_type + " COLLATE Turkish_CI_AI"
    if t.startswith("nchar") or t.startswith("char"):
        return col_type + " COLLATE Turkish_CI_AI"
    return col_type



def escape_sql(s: str) -> str:
    return s.replace("'", "''")


def default_for(col):
    name = col["new_name"]
    typ = col["sql_type"].lower()
    if col["identity"]:
        return None
    if name == "GId":
        return "DEFAULT newid()"
    if name == "Stat":
        return "DEFAULT 1"
    if name == "IsDelete":
        return "DEFAULT 0"
    if "datetime" in typ and name in ("InsertDateTime", "CreatedDate", "RecordDateTime", "RecDateTime"):
        return "DEFAULT getdate()"
    if name == "InsertUser":
        return "DEFAULT 0"
    return None


def generate_create_table(schema, table_name, columns, old_table, source_db):
    lines = []
    lines.append(f"--")
    lines.append(f"-- Definition for table {table_name} :")
    lines.append(f"-- Legacy: {source_db}.dbo.{old_table}")
    lines.append(f"--")
    lines.append(f"")
    lines.append(f"CREATE TABLE {schema}.{table_name} (")

    col_defs = []
    pk_col = None
    for col in columns:
        parts = [f"  {col['new_name']} {col['sql_type']}"]
        d = default_for(col)
        if d:
            parts.append(d)
        if col["identity"]:
            parts[0] = f"  {col['new_name']} {col['sql_type'].split()[0]} IDENTITY(1, 1)"
            pk_col = col["new_name"]
        null_sql = "NULL" if col["nullable"] and not col["identity"] else "NOT NULL"
        if col["identity"]:
            null_sql = "NOT NULL"
        parts.append(null_sql)
        col_defs.append(" ".join(parts))

    lines.append(",\n".join(col_defs))
    if pk_col:
        lines.append(f",\n  CONSTRAINT {table_name}_pk PRIMARY KEY CLUSTERED ({pk_col})")
        lines.append("    WITH (")
        lines.append("      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,")
        lines.append("      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)")
    lines.append(")")
    lines.append("ON [PRIMARY]")
    if any("image" in c["sql_type"].lower() or "text" in c["sql_type"].lower() for c in columns):
        lines.append("TEXTIMAGE_ON [PRIMARY]")
    lines.append("GO")
    lines.append("")

    # Table description (TR)
    desc = f"Eski tablo: {old_table} ({source_db}). Yeni şema: {schema}.{table_name}"
    lines.append(
        f"EXEC sp_addextendedproperty 'MS_Description', N'{escape_sql(desc)}', "
        f"N'schema', N'{schema}', N'table', N'{table_name}'"
    )
    lines.append("GO")
    lines.append("")

    for col in columns:
        old = col["old_name"]
        new = col["new_name"]
        desc_text = (col["description"] or "").strip() or old
        # Prefer Turkish legacy doc text; keep mapping note in TR
        full_desc = f"{desc_text} | Eski alan: {old_table}.{old}"
        lines.append(
            f"EXEC sp_addextendedproperty 'MS_Description', N'{escape_sql(full_desc)}', "
            f"N'schema', N'{schema}', N'table', N'{table_name}', N'column', N'{new}'"
        )
        lines.append("GO")
        lines.append("")

    return "\n".join(lines)


def generate_header():
    return f"""-- SQL Manager Lite style export
-- ---------------------------------------
-- Project   : EnKolayCari (Unified DB)
-- Database  : {DB_NAME}
-- Source    : TICARI_MASTER + TICARI_SLAVE1 consolidation
-- Generated : Auto-generated by scripts/generate_ddl.py
-- Rules     : Tablo/alan adlari EN; MS_Description aciklamalari TR (+ eski alan eslemesi)
-- ---------------------------------------

USE [master]
GO

IF DB_ID(N'{DB_NAME}') IS NOT NULL
BEGIN
  ALTER DATABASE [{DB_NAME}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP DATABASE [{DB_NAME}];
END
GO

CREATE DATABASE [{DB_NAME}]
  COLLATE Turkish_CI_AI
GO

USE [{DB_NAME}]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

"""


def generate_schemas():
    lines = []
    for s in SCHEMAS:
        if s == "dbo":
            continue  # dbo exists by default in SQL Server
        auth = "dbo"
        lines.append(f"--")
        lines.append(f"-- Definition for schema {s} :")
        lines.append(f"--")
        lines.append(f"")
        if s == "HangFire":
            lines.append(f"CREATE SCHEMA [{s}]")
        else:
            lines.append(f"CREATE SCHEMA {s}")
        lines.append(f"  AUTHORIZATION [{auth}]")
        lines.append("GO")
        lines.append("")
    return "\n".join(lines)


def generate_ai_architecture_tables():
    """Tables defined by AIProjeMimari / Auditness common patterns (not legacy EnKolayCari)."""
    return """
--
-- Definition for table Moduls :
-- Source: AIProjeMimari common.Moduls (Id is NOT IDENTITY; replaces legacy Modul)
--

CREATE TABLE common.Moduls (
  Id bigint NOT NULL,
  ModulName nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  CONSTRAINT Moduls_pk PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'Modul katalogu (AI mimari). Eski tablo: Modul (TICARI_MASTER). Id IDENTITY degildir.', N'schema', N'common', N'table', N'Moduls'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Modul Id (manuel seed). Eski alan: Modul.Id', N'schema', N'common', N'table', N'Moduls', N'column', N'Id'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Modul adi. Eski alan: Modul (ad/aciklama alanlari)', N'schema', N'common', N'table', N'Moduls', N'column', N'ModulName'
GO

--
-- Definition for table City :
-- Source: AIProjeMimari / Auditness common.City (replaces TICARI_MASTER.dbo.Iller + Ilceler)
-- Level: 1=Province (Il), 2=District (Ilce)
--

CREATE TABLE common.City (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier NOT NULL,
  Stat bit DEFAULT 1 NOT NULL,
  FullCityCode nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  CountryCode nvarchar(10) COLLATE Turkish_CI_AI NOT NULL,
  CityCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  ParentCityCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  CityName nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  Level int DEFAULT 1 NOT NULL,
  CONSTRAINT City_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'Sehir/ilce hiyerarsisi (AI mimari). Level 1=Il, Level 2=Ilce. Eski: Iller + Ilceler.', N'schema', N'common', N'table', N'City'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Birincil anahtar (Identity)', N'schema', N'common', N'table', N'City', N'column', N'Id'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Kuresel benzersiz kimlik (Guid)', N'schema', N'common', N'table', N'City', N'column', N'GId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Aktif/pasif (1=aktif)', N'schema', N'common', N'table', N'City', N'column', N'Stat'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Bilesik sehir kodu (ulke + il/ilce). Unique anahtar.', N'schema', N'common', N'table', N'City', N'column', N'FullCityCode'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Ulke kodu (ornek: TR)', N'schema', N'common', N'table', N'City', N'column', N'CountryCode'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Il veya ilce kodu', N'schema', N'common', N'table', N'City', N'column', N'CityCode'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Ust il CityCode (Level=2 iken). Level=1 icin null.', N'schema', N'common', N'table', N'City', N'column', N'ParentCityCode'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Il veya ilce adi. Eski: Iller.IlAdi / Ilceler.IlceAdi', N'schema', N'common', N'table', N'City', N'column', N'CityName'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Seviye: 1=Il, 2=Ilce. Eski Iller+Ilceler hiyerarsisi.', N'schema', N'common', N'table', N'City', N'column', N'Level'
GO

CREATE UNIQUE NONCLUSTERED INDEX City_uq ON common.City
  (FullCityCode)
WITH (
  PAD_INDEX = OFF,
  IGNORE_DUP_KEY = OFF,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_City_FullCityCode_Level ON common.City
  (FullCityCode, Level)
INCLUDE (CityName, CountryCode, ParentCityCode)
WITH (
  PAD_INDEX = OFF,
  FILLFACTOR = 90,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

--
-- Definition for table FileHeader :
-- Source: AIProjeMimari / Auditness common.FileHeader (replaces TICARI_SLAVE1.dbo.Dosyalar)
-- Polymorphic attachment: TableName + TableId
--

CREATE TABLE common.FileHeader (
  Id bigint IDENTITY(1, 1) NOT NULL,
  TableName nvarchar(50) COLLATE Turkish_CI_AI NULL,
  TableId bigint NULL,
  FileName nvarchar(500) COLLATE Turkish_CI_AI NULL,
  FileOrder int NOT NULL,
  FileType nvarchar(max) COLLATE Turkish_CI_AI NULL,
  CONSTRAINT FileHeader_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'Dosya ust bilgisi (AI mimari). TableName + TableId ile baglanir. Eski: Dosyalar.', N'schema', N'common', N'table', N'FileHeader'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Birincil anahtar (Identity)', N'schema', N'common', N'table', N'FileHeader', N'column', N'Id'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Sahip tablo adi (ornek: Product, Account, TradeDocument). Eski: Dosyalar.TabloAdi', N'schema', N'common', N'table', N'FileHeader', N'column', N'TableName'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Sahip kayit Id. Eski: Dosyalar.TabloId', N'schema', N'common', N'table', N'FileHeader', N'column', N'TableId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Dosya adi. Eski: Dosyalar.DosyaAdi', N'schema', N'common', N'table', N'FileHeader', N'column', N'FileName'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Goruntuleme sirasi. Eski: Dosyalar.Sira', N'schema', N'common', N'table', N'FileHeader', N'column', N'FileOrder'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Dosya tipi (png / jpg / pdf vb.). Eski: Dosyalar.DosyaUzantisi', N'schema', N'common', N'table', N'FileHeader', N'column', N'FileType'
GO

CREATE NONCLUSTERED INDEX IX_FileHeader_TableName_TableId ON common.FileHeader
  (TableName, TableId)
INCLUDE (FileName, FileOrder, FileType)
WITH (
  PAD_INDEX = OFF,
  FILLFACTOR = 90,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

--
-- Definition for table FileBlob :
-- Source: AIProjeMimari / Auditness common.FileBlob (replaces TICARI_SLAVE1.dbo.DosyalarBlob)
--

CREATE TABLE common.FileBlob (
  Id bigint IDENTITY(1, 1) NOT NULL,
  FileHeaderId bigint NOT NULL,
  Blob image NOT NULL,
  CONSTRAINT FileBlob_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'Dosya icerigi (binary). FK -> FileHeader. Eski: DosyalarBlob.', N'schema', N'common', N'table', N'FileBlob'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Birincil anahtar (Identity)', N'schema', N'common', N'table', N'FileBlob', N'column', N'Id'
GO

EXEC sp_addextendedproperty 'MS_Description', N'FK: common.FileHeader.Id', N'schema', N'common', N'table', N'FileBlob', N'column', N'FileHeaderId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Binary dosya icerigi. Eski: DosyalarBlob.Blob', N'schema', N'common', N'table', N'FileBlob', N'column', N'Blob'
GO

ALTER TABLE common.FileBlob
ADD CONSTRAINT FileBlob_fk FOREIGN KEY (FileHeaderId)
  REFERENCES common.FileHeader (Id)
  ON UPDATE NO ACTION
  ON DELETE CASCADE
GO

--
-- Definition for table Personal :
-- Source: AIProjeMimari / Auditness common.Personal (replaces TICARI_MASTER.dbo.PersonelTanim)
-- Audit Family A (Created*/Modifed*)
--

CREATE TABLE common.Personal (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyMasterCode nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  Stat bit DEFAULT 1 NOT NULL,
  Name nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  SurName nvarchar(30) COLLATE Turkish_CI_AI NOT NULL,
  UseEMailForLogin bit DEFAULT 0 NOT NULL,
  Email nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  DomainUserId nvarchar(100) COLLATE Turkish_CI_AI NULL,
  IsWebEnabled bit DEFAULT 0 NOT NULL,
  IsBranchManager bit DEFAULT 0 NOT NULL,
  ManagerAccountCode nvarchar(50) COLLATE Turkish_CI_AI NULL,
  CostCenter nvarchar(100) COLLATE Turkish_CI_AI NULL,
  PhoneNumber nvarchar(100) COLLATE Turkish_CI_AI NULL,
  BranchId bigint NULL,
  ParentPersonalId bigint NULL,
  IdentityNumber nvarchar(30) COLLATE Turkish_CI_AI NULL,
  ManagerIdentityNumber nvarchar(30) COLLATE Turkish_CI_AI NULL,
  StartWorkDate datetime NULL,
  EndWorkDate datetime NULL,
  AppCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  AppCodeId bigint NULL,
  CountryCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  UserId nvarchar(450) COLLATE Turkish_CI_AI NULL,
  UserIdForDomain nvarchar(450) COLLATE Turkish_CI_AI NULL,
  DeviceId nvarchar(500) COLLATE Turkish_CI_AI NULL,
  CustomClientOs nvarchar(100) COLLATE Turkish_CI_AI NULL,
  CustomClientAppVersion nvarchar(100) COLLATE Turkish_CI_AI NULL,
  Color nvarchar(200) COLLATE Turkish_CI_AI NULL,
  ResetPassCode nvarchar(30) COLLATE Turkish_CI_AI NULL,
  ResetPassExpireDate datetime NULL,
  EmplId nvarchar(20) COLLATE Turkish_CI_AI NULL,
  Division nvarchar(10) COLLATE Turkish_CI_AI NULL,
  PositionDesc nvarchar(255) COLLATE Turkish_CI_AI NULL,
  CreatedDate datetime DEFAULT getdate() NOT NULL,
  CreatedUser bigint DEFAULT 0 NOT NULL,
  ModifedDate datetime NULL,
  ModifedUser bigint NULL,
  DeletedDate datetime NULL,
  DeletedUser bigint NULL,
  IsDelete bigint DEFAULT 0 NOT NULL,
  CONSTRAINT Personal_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'Personel/kullanici karti (AI mimari). Eski: PersonelTanim. Tenant: CompanyMasterCode.', N'schema', N'common', N'table', N'Personal'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Birincil anahtar (Identity). Eski: PersonelTanim.Id', N'schema', N'common', N'table', N'Personal', N'column', N'Id'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Kuresel benzersiz kimlik. Eski: PersonelTanim.GId', N'schema', N'common', N'table', N'Personal', N'column', N'GId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Sirket master kodu (tenant). Eski: PersonelTanim.SirketTanimId -> Company.CompanyMasterCode', N'schema', N'common', N'table', N'Personal', N'column', N'CompanyMasterCode'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Aktif/pasif. Eski: PersonelTanim.Aktif', N'schema', N'common', N'table', N'Personal', N'column', N'Stat'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Ad. Eski: PersonelTanim.AdSoyad (bolunmus)', N'schema', N'common', N'table', N'Personal', N'column', N'Name'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Soyad. Eski: PersonelTanim.AdSoyad (bolunmus)', N'schema', N'common', N'table', N'Personal', N'column', N'SurName'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Domain kullanicisi olmayanlar icin e-posta ile giris. | Eski alan: PersonelTanim e-posta giris modu', N'schema', N'common', N'table', N'Personal', N'column', N'UseEMailForLogin'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Giris e-postasi. Eski: PersonelTanim.Email', N'schema', N'common', N'table', N'Personal', N'column', N'Email'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Domain kullanici Id. Eski: PersonelTanim (dogrudan alan yok)', N'schema', N'common', N'table', N'Personal', N'column', N'DomainUserId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Telefon. Eski: PersonelTanim.Telefon', N'schema', N'common', N'table', N'Personal', N'column', N'PhoneNumber'
GO

EXEC sp_addextendedproperty 'MS_Description', N'AspNetUsers.Id (e-posta girisi). Eski: PersonelTanim Identity baglantisi', N'schema', N'common', N'table', N'Personal', N'column', N'UserId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'AspNetUsers.Id (domain girisi).', N'schema', N'common', N'table', N'Personal', N'column', N'UserIdForDomain'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Takvim/UI rengi. Eski: PersonelTanim.Renk', N'schema', N'common', N'table', N'Personal', N'column', N'Color'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Sifre sifirlama kodu son kullanma. Eski: PersonelTanim.PasswordResetCodeExpirationMinutes', N'schema', N'common', N'table', N'Personal', N'column', N'ResetPassExpireDate'
GO

CREATE NONCLUSTERED INDEX IX_Personal_CompanyMasterCode_IsDelete ON common.Personal
  (CompanyMasterCode, IsDelete)
INCLUDE (BranchId, DomainUserId, Email, Id, Name, SurName)
WITH (
  PAD_INDEX = OFF,
  FILLFACTOR = 90,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_Personal_DomainUserId_IsDelete ON common.Personal
  (DomainUserId, IsDelete)
WHERE ([DomainUserId] IS NOT NULL AND [DomainUserId]<>'')
WITH (
  PAD_INDEX = OFF,
  FILLFACTOR = 90,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_Missing_Personal_UserId ON common.Personal
  (UserId)
INCLUDE (DomainUserId, UserIdForDomain)
WITH (
  PAD_INDEX = OFF,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX Personal_idx ON common.Personal
  (ManagerAccountCode)
WITH (
  PAD_INDEX = OFF,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

"""


def generate_legacy_transfer_map():
    """Record-level legacy <-> new mapping used during data transfer."""
    return """
--
-- Definition for table LegacyTransferMap :
-- Transfer esleme: eski tablo/GId -> yeni tablo/GId/CompanyId
--

CREATE TABLE common.LegacyTransferMap (
  Id bigint IDENTITY(1, 1) NOT NULL,
  GId uniqueidentifier DEFAULT newid() NOT NULL,
  CompanyId bigint NOT NULL,
  LegacyTableName nvarchar(128) COLLATE Turkish_CI_AI NOT NULL,
  LegacyGId uniqueidentifier NOT NULL,
  LegacyLastChangeDate datetime NULL,
  NewTableName nvarchar(128) COLLATE Turkish_CI_AI NOT NULL,
  NewGId uniqueidentifier NOT NULL,
  InsertDateTime datetime DEFAULT getdate() NOT NULL,
  CONSTRAINT LegacyTransferMap_pk PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'Transfer esleme tablosu. Eski tablo/GId ile yeni tablo/GId/CompanyId arasini tutar. Veri aktarimi sirasinda kullanilir.', N'schema', N'common', N'table', N'LegacyTransferMap'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Birincil anahtar (Identity)', N'schema', N'common', N'table', N'LegacyTransferMap', N'column', N'Id'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Kuresel benzersiz kimlik', N'schema', N'common', N'table', N'LegacyTransferMap', N'column', N'GId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Yeni kaydin CompanyId degeri (tenant)', N'schema', N'common', N'table', N'LegacyTransferMap', N'column', N'CompanyId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Eski tablo adi (ornek: CariTanim, UrunTanim)', N'schema', N'common', N'table', N'LegacyTransferMap', N'column', N'LegacyTableName'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Eski kayit GId', N'schema', N'common', N'table', N'LegacyTransferMap', N'column', N'LegacyGId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Eski kayittaki Insert/Update/Delete tarihlerinden en guncel olan', N'schema', N'common', N'table', N'LegacyTransferMap', N'column', N'LegacyLastChangeDate'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Yeni tablo adi (ornek: finance.Account veya Account)', N'schema', N'common', N'table', N'LegacyTransferMap', N'column', N'NewTableName'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Yeni kayit GId', N'schema', N'common', N'table', N'LegacyTransferMap', N'column', N'NewGId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Esleme kaydinin olusturulma zamani', N'schema', N'common', N'table', N'LegacyTransferMap', N'column', N'InsertDateTime'
GO

CREATE UNIQUE NONCLUSTERED INDEX UQ_LegacyTransferMap_LegacyTable_LegacyGId
  ON common.LegacyTransferMap (LegacyTableName, LegacyGId)
WITH (
  PAD_INDEX = OFF,
  IGNORE_DUP_KEY = OFF,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_LegacyTransferMap_CompanyId_NewTable_NewGId
  ON common.LegacyTransferMap (CompanyId, NewTableName, NewGId)
WITH (
  PAD_INDEX = OFF,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_LegacyTransferMap_LegacyLastChangeDate
  ON common.LegacyTransferMap (LegacyLastChangeDate)
WITH (
  PAD_INDEX = OFF,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

"""


def generate_aspnet_tables():
    return """
--
-- Definition for table AspNetRoles (Identity)
--

CREATE TABLE dbo.AspNetRoles (
  Id nvarchar(450) COLLATE Turkish_CI_AI DEFAULT newid() NOT NULL,
  Name nvarchar(256) COLLATE Turkish_CI_AI NULL,
  NormalizedName nvarchar(256) COLLATE Turkish_CI_AI NULL,
  ConcurrencyStamp nvarchar(max) COLLATE Turkish_CI_AI NULL,
  Discriminator nvarchar(128) COLLATE Turkish_CI_AI DEFAULT 'AspNetRole' NOT NULL,
  CONSTRAINT PK_AspNetRoles PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'ASP.NET Identity roller. Eski: MASTER.dbo.Roles + SLAVE.dbo.UserRoles hedefi.', N'schema', N'dbo', N'table', N'AspNetRoles'
GO

CREATE TABLE dbo.AspNetUsers (
  Id nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  FullName nvarchar(60) COLLATE Turkish_CI_AI NOT NULL,
  UserName nvarchar(256) COLLATE Turkish_CI_AI NULL,
  NormalizedUserName nvarchar(256) COLLATE Turkish_CI_AI NULL,
  Email nvarchar(256) COLLATE Turkish_CI_AI NULL,
  NormalizedEmail nvarchar(256) COLLATE Turkish_CI_AI NULL,
  EmailConfirmed bit NOT NULL,
  PasswordHash nvarchar(max) COLLATE Turkish_CI_AI NULL,
  SecurityStamp nvarchar(max) COLLATE Turkish_CI_AI NULL,
  ConcurrencyStamp nvarchar(max) COLLATE Turkish_CI_AI NULL,
  PhoneNumber nvarchar(max) COLLATE Turkish_CI_AI NULL,
  PhoneNumberConfirmed bit NOT NULL,
  TwoFactorEnabled bit NOT NULL,
  LockoutEnd datetimeoffset(0) NULL,
  LockoutEnabled bit NOT NULL,
  AccessFailedCount int NOT NULL,
  Discriminator nvarchar(128) COLLATE Turkish_CI_AI DEFAULT 'AspNetUsers' NOT NULL,
  CompanyId bigint DEFAULT 0 NOT NULL,
  CONSTRAINT PK_AspNetUsers PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'ASP.NET Identity kullanicilar. Eski: PersonelTanim.UserId eslemesi.', N'schema', N'dbo', N'table', N'AspNetUsers'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Sirket kapsamli tenant. Eski: SirketTanimId -> CompanyId | Eski alan: PersonelTanim.SirketTanimId', N'schema', N'dbo', N'table', N'AspNetUsers', N'column', N'CompanyId'
GO

CREATE TABLE dbo.AspNetUserRoles (
  UserId nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  RoleId nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  Discriminator nvarchar(128) COLLATE Turkish_CI_AI DEFAULT 'AspNetUserRoles' NOT NULL,
  CompanyId bigint NOT NULL,
  CONSTRAINT PK_AspNetUserRoles PRIMARY KEY CLUSTERED (UserId, RoleId, CompanyId)
)
ON [PRIMARY]
GO

CREATE TABLE dbo.MenuRole (
  Id bigint IDENTITY(1, 1) NOT NULL,
  CompanyId bigint NOT NULL,
  MenuKey nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  RoleId nvarchar(450) COLLATE Turkish_CI_AI NOT NULL,
  CreatedDate datetime DEFAULT getdate() NOT NULL,
  CreatedUser bigint DEFAULT 0 NOT NULL,
  CONSTRAINT PK_MenuRole PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'Sirket kapsamli menu-rol eslemesi. Eski: SayfaTanim + Roles.', N'schema', N'dbo', N'table', N'MenuRole'
GO

"""


def generate_hangfire_tables():
    return """
-- HangFire schema tables (standard)

CREATE TABLE HangFire.AggregatedCounter (
  [Key] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  Value bigint NOT NULL,
  ExpireAt datetime NULL,
  CONSTRAINT PK_HangFire_CounterAggregated PRIMARY KEY CLUSTERED ([Key])
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.Counter (
  [Key] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  Value int NOT NULL,
  ExpireAt datetime NULL,
  Id bigint IDENTITY(1, 1) NOT NULL,
  CONSTRAINT PK_HangFire_Counter PRIMARY KEY CLUSTERED ([Key], Id)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.Hash (
  [Key] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  Field nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  Value nvarchar(max) COLLATE Turkish_CI_AI NULL,
  ExpireAt datetime2(7) NULL,
  CONSTRAINT PK_HangFire_Hash PRIMARY KEY CLUSTERED ([Key], Field)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.Job (
  Id bigint IDENTITY(1, 1) NOT NULL,
  StateId bigint NULL,
  StateName nvarchar(20) COLLATE Turkish_CI_AI NULL,
  InvocationData nvarchar(max) COLLATE Turkish_CI_AI NOT NULL,
  Arguments nvarchar(max) COLLATE Turkish_CI_AI NOT NULL,
  CreatedAt datetime NOT NULL,
  ExpireAt datetime NULL,
  CONSTRAINT PK_HangFire_Job PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.JobParameter (
  JobId bigint NOT NULL,
  Name nvarchar(40) COLLATE Turkish_CI_AI NOT NULL,
  Value nvarchar(max) COLLATE Turkish_CI_AI NULL,
  CONSTRAINT PK_HangFire_JobParameter PRIMARY KEY CLUSTERED (JobId, Name)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.JobQueue (
  Id bigint IDENTITY(1, 1) NOT NULL,
  JobId bigint NOT NULL,
  Queue nvarchar(50) COLLATE Turkish_CI_AI NOT NULL,
  FetchedAt datetime NULL,
  CONSTRAINT PK_HangFire_JobQueue PRIMARY KEY CLUSTERED (Queue, Id)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.List (
  Id bigint IDENTITY(1, 1) NOT NULL,
  [Key] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  Value nvarchar(max) COLLATE Turkish_CI_AI NULL,
  ExpireAt datetime NULL,
  CONSTRAINT PK_HangFire_List PRIMARY KEY CLUSTERED ([Key], Id)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.[Schema] (
  Version int NOT NULL,
  CONSTRAINT PK_HangFire_Schema PRIMARY KEY CLUSTERED (Version)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.Server (
  Id nvarchar(200) COLLATE Turkish_CI_AI NOT NULL,
  Data nvarchar(max) COLLATE Turkish_CI_AI NULL,
  LastHeartbeat datetime NOT NULL,
  CONSTRAINT PK_HangFire_Server PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.[Set] (
  [Key] nvarchar(100) COLLATE Turkish_CI_AI NOT NULL,
  Score float NOT NULL,
  Value nvarchar(256) COLLATE Turkish_CI_AI NOT NULL,
  ExpireAt datetime NULL,
  CONSTRAINT PK_HangFire_Set PRIMARY KEY CLUSTERED ([Key], Value)
)
ON [PRIMARY]
GO

CREATE TABLE HangFire.State (
  Id bigint IDENTITY(1, 1) NOT NULL,
  JobId bigint NOT NULL,
  Name nvarchar(20) COLLATE Turkish_CI_AI NOT NULL,
  Reason nvarchar(100) COLLATE Turkish_CI_AI NULL,
  CreatedAt datetime NOT NULL,
  Data nvarchar(max) COLLATE Turkish_CI_AI NULL,
  CONSTRAINT PK_HangFire_State PRIMARY KEY CLUSTERED (JobId, Id)
)
ON [PRIMARY]
GO

"""


def generate_seed_data():
    return """
-- Seed: common.Moduls (AIProjeMimari standard)
-- Table mapping lives in docs/TABLE-MAPPING.md (platform schema deferred)

INSERT INTO common.Moduls (Id, ModulName) VALUES
  (1, N'AccountType'),
  (2, N'TradeDocumentStatus'),
  (3, N'PaymentStatus'),
  (4, N'ActionStatus'),
  (5, N'TaskStatus')
GO

"""


def generate_fk_section(fk_list):
    lines = ["--", "-- Definition for foreign keys :", "--", ""]
    for fk in fk_list:
        lines.append(f"ALTER TABLE {fk['from_table']}")
        lines.append(f"ADD CONSTRAINT {fk['name']} FOREIGN KEY ({fk['column']})")
        lines.append(f"  REFERENCES {fk['ref_table']} ({fk['ref_column']})")
        lines.append("  ON UPDATE NO ACTION")
        lines.append("  ON DELETE NO ACTION")
        lines.append("GO")
        lines.append("")
    return "\n".join(lines)


def main():
    table_map = load_table_mappings()
    seen_new_tables = set()
    all_tables = []
    mapping_rows = []
    fk_candidates = []

    for folder in ["master", "slave"]:
        doc_dir = LEGACY_DOCS / folder
        if not doc_dir.exists():
            continue
        for md_file in sorted(doc_dir.glob("table-*.md")):
            old_name, source_db, columns = parse_table_md(md_file)
            if old_name in EXCLUDED_TABLES:
                continue
            if old_name not in table_map:
                print(f"WARNING: No mapping for {old_name}")
                continue
            m = table_map[old_name]
            schema = m["schema"]
            new_name = m["newTableName"]
            key = f"{schema}.{new_name}"
            if m.get("skipIfDuplicate") and key in seen_new_tables:
                continue
            seen_new_tables.add(key)

            new_columns = []
            used_names = set()
            excluded_cols = EXCLUDED_COLUMNS.get(old_name, set())
            for col in columns:
                old_col = col["old_name"]
                # Normalize corrupted/Turkish identifiers early for exclusions + mapping
                old_col_clean = sanitize_ident(old_col) if "\ufffd" in old_col else old_col
                if old_col in excluded_cols or old_col_clean in excluded_cols:
                    continue
                new_col_name = translate_column(old_col_clean if "\ufffd" in old_col else old_col, old_name)
                new_col_name = sanitize_ident(new_col_name)
                base = new_col_name
                i = 2
                while new_col_name in used_names:
                    new_col_name = f"{base}{i}"
                    i += 1
                used_names.add(new_col_name)
                new_columns.append({
                    **col,
                    "new_name": new_col_name,
                    "sql_type": sql_type(col["type"], new_col_name),
                })
                if new_col_name.endswith("Id") and new_col_name != "Id":
                    hint = FK_TABLE_HINTS.get(new_col_name)
                    if hint:
                        fk_candidates.append({
                            "from_table": f"{schema}.{new_name}",
                            "column": new_col_name,
                            "ref_table": hint,
                            "ref_column": "Id",
                            "name": f"FK_{new_name}_{new_col_name}",
                        })

            all_tables.append((schema, new_name, old_name, source_db, new_columns))
            mapping_rows.append((old_name, source_db, schema, new_name, m.get("notes", "")))

    # Sort: schemas order, then table name
    schema_order = {s: i for i, s in enumerate(SCHEMAS)}
    all_tables.sort(key=lambda x: (schema_order.get(x[0], 99), x[1]))

    sql_parts = [generate_header(), generate_schemas()]

    # AspNet + HangFire (skip duplicate HangFire from master parse)
    hangfire_names = {t[1] for t in all_tables if t[0] == "HangFire"}
    sql_parts.append(generate_aspnet_tables())
    sql_parts.append(generate_ai_architecture_tables())
    sql_parts.append(generate_legacy_transfer_map())
    if not hangfire_names:
        sql_parts.append(generate_hangfire_tables())

    for schema, new_name, old_name, source_db, cols in all_tables:
        if schema == "HangFire":
            continue  # use standard HangFire DDL
        if schema == "dbo" and new_name.startswith("AspNet"):
            continue
        sql_parts.append(generate_create_table(schema, new_name, cols, old_name, source_db))

    if hangfire_names:
        sql_parts.append(generate_hangfire_tables())

    sql_parts.append(generate_seed_data())

    # FK section (dedupe)
    seen_fk = set()
    unique_fks = []
    for fk in fk_candidates:
        k = (fk["from_table"], fk["column"])
        if k not in seen_fk:
            seen_fk.add(k)
            unique_fks.append(fk)
    sql_parts.append(generate_fk_section(unique_fks[:50]))  # cap for safety

    OUTPUT_SQL.parent.mkdir(parents=True, exist_ok=True)
    OUTPUT_SQL.write_text("\n".join(sql_parts), encoding="utf-8-sig", newline="\n")

    # TABLE-MAPPING.md
    md_lines = [
        "# Table Mapping — Legacy TR → EnKolayCari EN",
        "",
        "| Legacy DB | Legacy Table (TR) | New Schema | New Table (EN) | Notes |",
        "|-----------|-------------------|------------|----------------|-------|",
    ]
    for old, src, sch, new, notes in sorted(mapping_rows, key=lambda r: (r[2], r[3])):
        md_lines.append(f"| {src} | `{old}` | `{sch}` | `{new}` | {notes or ''} |")

    OUTPUT_MAPPING_MD.parent.mkdir(parents=True, exist_ok=True)
    OUTPUT_MAPPING_MD.write_text("\n".join(md_lines), encoding="utf-8", newline="\n")

    print(f"Generated: {OUTPUT_SQL}")
    print(f"Tables: {len(all_tables)}")
    print(f"Mapping doc: {OUTPUT_MAPPING_MD}")


if __name__ == "__main__":
    main()
