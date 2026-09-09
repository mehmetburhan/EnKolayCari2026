-- =============================================================================
-- Patch 002 — Kolon Adlarını Tam İngilizce'ye Çevirme
-- Database  : EKCN2026
-- Tarih     : 2026-09-09
-- Kural     : Tüm Türkçe / hibrit kolon adları tam İngilizce adlara
--             dönüştürülür. Eski DB kolon adı MS_Description alanında
--             "Eski alan: Tablo.EskiKolonAdi" formatında saklanır.
-- Uygulama  : Bu dosyayı EKCN2026 veritabanında çalıştırın.
--             Sonra EF scaffold yeniden yapılacaktır.
-- =============================================================================

USE [EKCN2026]
GO

PRINT '=== PATCH 002 - Column Rename başlıyor ===';
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- common.Counter
-- ─────────────────────────────────────────────────────────────────────────────
-- Seri → SerialCode
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='common' AND TABLE_NAME='Counter' AND COLUMN_NAME='Seri')
BEGIN
    EXEC sp_rename 'common.Counter.Seri', 'SerialCode', 'COLUMN';
    PRINT 'common.Counter.Seri → SerialCode';
END

-- BaslangicNo → StartNumber
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='common' AND TABLE_NAME='Counter' AND COLUMN_NAME='BaslangicNo')
BEGIN
    EXEC sp_rename 'common.Counter.BaslangicNo', 'StartNumber', 'COLUMN';
    PRINT 'common.Counter.BaslangicNo → StartNumber';
END

-- BitisNo → EndNumber
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='common' AND TABLE_NAME='Counter' AND COLUMN_NAME='BitisNo')
BEGIN
    EXEC sp_rename 'common.Counter.BitisNo', 'EndNumber', 'COLUMN';
    PRINT 'common.Counter.BitisNo → EndNumber';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- common.EmailTemplate
-- ─────────────────────────────────────────────────────────────────────────────
-- Metin → Body
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='common' AND TABLE_NAME='EmailTemplate' AND COLUMN_NAME='Metin')
BEGIN
    EXEC sp_rename 'common.EmailTemplate.Metin', 'Body', 'COLUMN';
    PRINT 'common.EmailTemplate.Metin → Body';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- common.License
-- ─────────────────────────────────────────────────────────────────────────────
-- Hediye → IsGift
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='common' AND TABLE_NAME='License' AND COLUMN_NAME='Hediye')
BEGIN
    EXEC sp_rename 'common.License.Hediye', 'IsGift', 'COLUMN';
    PRINT 'common.License.Hediye → IsGift';
END

-- TahsilatSekli → CollectionMethod
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='common' AND TABLE_NAME='License' AND COLUMN_NAME='TahsilatSekli')
BEGIN
    EXEC sp_rename 'common.License.TahsilatSekli', 'CollectionMethod', 'COLUMN';
    PRINT 'common.License.TahsilatSekli → CollectionMethod';
END

-- TahsilatTutari → CollectionAmount
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='common' AND TABLE_NAME='License' AND COLUMN_NAME='TahsilatTutari')
BEGIN
    EXEC sp_rename 'common.License.TahsilatTutari', 'CollectionAmount', 'COLUMN';
    PRINT 'common.License.TahsilatTutari → CollectionAmount';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- common.LicenseType
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='common' AND TABLE_NAME='LicenseType' AND COLUMN_NAME='ProductTakip')
BEGIN
    EXEC sp_rename 'common.LicenseType.ProductTakip',         'ProductTracking',        'COLUMN';
    PRINT 'LicenseType.ProductTakip → ProductTracking';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='common' AND TABLE_NAME='LicenseType' AND COLUMN_NAME='AccountTakip')
BEGIN
    EXEC sp_rename 'common.LicenseType.AccountTakip',         'AccountTracking',        'COLUMN';
    PRINT 'LicenseType.AccountTakip → AccountTracking';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='common' AND TABLE_NAME='LicenseType' AND COLUMN_NAME='CheckNoteTakip')
BEGIN
    EXEC sp_rename 'common.LicenseType.CheckNoteTakip',       'CheckNoteTracking',      'COLUMN';
    PRINT 'LicenseType.CheckNoteTakip → CheckNoteTracking';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='common' AND TABLE_NAME='LicenseType' AND COLUMN_NAME='TradeDocumentTakip')
BEGIN
    EXEC sp_rename 'common.LicenseType.TradeDocumentTakip',   'TradeDocumentTracking',  'COLUMN';
    PRINT 'LicenseType.TradeDocumentTakip → TradeDocumentTracking';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='common' AND TABLE_NAME='LicenseType' AND COLUMN_NAME='IrsaliyeTakip')
BEGIN
    EXEC sp_rename 'common.LicenseType.IrsaliyeTakip',        'ShippingNoteTracking',   'COLUMN';
    PRINT 'LicenseType.IrsaliyeTakip → ShippingNoteTracking';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='common' AND TABLE_NAME='LicenseType' AND COLUMN_NAME='TeklifSiparisTakip')
BEGIN
    EXEC sp_rename 'common.LicenseType.TeklifSiparisTakip',   'QuoteOrderTracking',     'COLUMN';
    PRINT 'LicenseType.TeklifSiparisTakip → QuoteOrderTracking';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- finance.AccountDocument
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='finance' AND TABLE_NAME='AccountDocument' AND COLUMN_NAME='DocumentTanimDescription')
BEGIN
    EXEC sp_rename 'finance.AccountDocument.DocumentTanimDescription', 'DocumentTypeDescription', 'COLUMN';
    PRINT 'AccountDocument.DocumentTanimDescription → DocumentTypeDescription';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='finance' AND TABLE_NAME='AccountDocument' AND COLUMN_NAME='DocumentIcerik')
BEGIN
    EXEC sp_rename 'finance.AccountDocument.DocumentIcerik', 'DocumentContent', 'COLUMN';
    PRINT 'AccountDocument.DocumentIcerik → DocumentContent';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- finance.AccountTransaction
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='finance' AND TABLE_NAME='AccountTransaction' AND COLUMN_NAME='HareketType')
BEGIN
    EXEC sp_rename 'finance.AccountTransaction.HareketType', 'TransactionType', 'COLUMN';
    PRINT 'AccountTransaction.HareketType → TransactionType';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- finance.CashTransaction
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='finance' AND TABLE_NAME='CashTransaction' AND COLUMN_NAME='AccountHareketId')
BEGIN
    EXEC sp_rename 'finance.CashTransaction.AccountHareketId', 'AccountTransactionId', 'COLUMN';
    PRINT 'CashTransaction.AccountHareketId → AccountTransactionId';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='finance' AND TABLE_NAME='CashTransaction' AND COLUMN_NAME='CashRegisterHareketType')
BEGIN
    EXEC sp_rename 'finance.CashTransaction.CashRegisterHareketType', 'CashRegisterTransactionType', 'COLUMN';
    PRINT 'CashTransaction.CashRegisterHareketType → CashRegisterTransactionType';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- finance.CheckNote
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='finance' AND TABLE_NAME='CheckNote' AND COLUMN_NAME='HareketType')
BEGIN
    EXEC sp_rename 'finance.CheckNote.HareketType', 'NoteType', 'COLUMN';
    PRINT 'CheckNote.HareketType → NoteType';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- finance.CheckNoteTransaction
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='finance' AND TABLE_NAME='CheckNoteTransaction' AND COLUMN_NAME='AccountHareketId')
BEGIN
    EXEC sp_rename 'finance.CheckNoteTransaction.AccountHareketId', 'AccountTransactionId', 'COLUMN';
    PRINT 'CheckNoteTransaction.AccountHareketId → AccountTransactionId';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='finance' AND TABLE_NAME='CheckNoteTransaction' AND COLUMN_NAME='CashRegisterHareketId')
BEGIN
    EXEC sp_rename 'finance.CheckNoteTransaction.CashRegisterHareketId', 'CashRegisterTransactionId', 'COLUMN';
    PRINT 'CheckNoteTransaction.CashRegisterHareketId → CashRegisterTransactionId';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='finance' AND TABLE_NAME='CheckNoteTransaction' AND COLUMN_NAME='HareketDate')
BEGIN
    EXEC sp_rename 'finance.CheckNoteTransaction.HareketDate', 'TransactionDate', 'COLUMN';
    PRINT 'CheckNoteTransaction.HareketDate → TransactionDate';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- finance.Currency
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='finance' AND TABLE_NAME='Currency' AND COLUMN_NAME='Hassasiyet')
BEGIN
    EXEC sp_rename 'finance.Currency.Hassasiyet', 'DecimalPrecision', 'COLUMN';
    PRINT 'Currency.Hassasiyet → DecimalPrecision';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- inventory.Product
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='inventory' AND TABLE_NAME='Product' AND COLUMN_NAME='Derinlik')
BEGIN
    EXEC sp_rename 'inventory.Product.Derinlik', 'Depth', 'COLUMN';
    PRINT 'Product.Derinlik → Depth';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='inventory' AND TABLE_NAME='Product' AND COLUMN_NAME='Desi')
BEGIN
    EXEC sp_rename 'inventory.Product.Desi', 'DesiWeight', 'COLUMN';
    PRINT 'Product.Desi → DesiWeight';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='inventory' AND TABLE_NAME='Product' AND COLUMN_NAME='Agirlik')
BEGIN
    EXEC sp_rename 'inventory.Product.Agirlik', 'Weight', 'COLUMN';
    PRINT 'Product.Agirlik → Weight';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='inventory' AND TABLE_NAME='Product' AND COLUMN_NAME='SeriNoTakip')
BEGIN
    EXEC sp_rename 'inventory.Product.SeriNoTakip', 'SerialNumberTracking', 'COLUMN';
    PRINT 'Product.SeriNoTakip → SerialNumberTracking';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- inventory.ProductUnit
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='inventory' AND TABLE_NAME='ProductUnit' AND COLUMN_NAME='Hassasiyet')
BEGIN
    EXEC sp_rename 'inventory.ProductUnit.Hassasiyet', 'DecimalPrecision', 'COLUMN';
    PRINT 'ProductUnit.Hassasiyet → DecimalPrecision';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- inventory.Unit
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='inventory' AND TABLE_NAME='Unit' AND COLUMN_NAME='Hassasiyet')
BEGIN
    EXEC sp_rename 'inventory.Unit.Hassasiyet', 'DecimalPrecision', 'COLUMN';
    PRINT 'Unit.Hassasiyet → DecimalPrecision';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- trade.TradeDocument  (en fazla Türkçe kolon burada)
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='StoreTanim1Id')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.StoreTanim1Id',               'SecondaryStoreId',          'COLUMN';
    PRINT 'TradeDocument.StoreTanim1Id → SecondaryStoreId';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='HareketType')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.HareketType',                 'DocumentType',              'COLUMN';
    PRINT 'TradeDocument.HareketType → DocumentType';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='Saat')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.Saat',                        'TransactionTime',           'COLUMN';
    PRINT 'TradeDocument.Saat → TransactionTime';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='SiparisStatus')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.SiparisStatus',               'OrderStatus',               'COLUMN';
    PRINT 'TradeDocument.SiparisStatus → OrderStatus';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='ShippingFirmaDefId')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.ShippingFirmaDefId',          'ShippingCompanyDefId',      'COLUMN';
    PRINT 'TradeDocument.ShippingFirmaDefId → ShippingCompanyDefId';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='TradeDocumentTeslimAdSoyadTitle')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.TradeDocumentTeslimAdSoyadTitle', 'BillingFullNameOrTitle', 'COLUMN';
    PRINT 'TradeDocument.TradeDocumentTeslimAdSoyadTitle → BillingFullNameOrTitle';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='TradeDocumentTeslimEMail')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.TradeDocumentTeslimEMail',    'BillingEmail',              'COLUMN';
    PRINT 'TradeDocument.TradeDocumentTeslimEMail → BillingEmail';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='TradeDocumentTeslimPhone')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.TradeDocumentTeslimPhone',    'BillingPhone',              'COLUMN';
    PRINT 'TradeDocument.TradeDocumentTeslimPhone → BillingPhone';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='TeslimatIl')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.TeslimatIl',                  'DeliveryCity',              'COLUMN';
    PRINT 'TradeDocument.TeslimatIl → DeliveryCity';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='MedicalServiceCihazSeriNo')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.MedicalServiceCihazSeriNo',   'ServiceDeviceSerialNo',     'COLUMN';
    PRINT 'TradeDocument.MedicalServiceCihazSeriNo → ServiceDeviceSerialNo';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='MedicalServiceAksesuar')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.MedicalServiceAksesuar',      'ServiceAccessories',        'COLUMN';
    PRINT 'TradeDocument.MedicalServiceAksesuar → ServiceAccessories';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='MedicalServiceMusteriNotu')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.MedicalServiceMusteriNotu',   'ServiceCustomerNote',       'COLUMN';
    PRINT 'TradeDocument.MedicalServiceMusteriNotu → ServiceCustomerNote';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='MedicalServiceGarantiBilgisi')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.MedicalServiceGarantiBilgisi','ServiceWarrantyInfo',        'COLUMN';
    PRINT 'TradeDocument.MedicalServiceGarantiBilgisi → ServiceWarrantyInfo';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='MedicalServiceTeslimAlanKisi')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.MedicalServiceTeslimAlanKisi','ServiceDeliveryRecipient',  'COLUMN';
    PRINT 'TradeDocument.MedicalServiceTeslimAlanKisi → ServiceDeliveryRecipient';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='TeklifStatus')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.TeklifStatus',                'QuoteStatus',               'COLUMN';
    PRINT 'TradeDocument.TeklifStatus → QuoteStatus';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='DocumentKapali')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.DocumentKapali',              'IsDocumentClosed',          'COLUMN';
    PRINT 'TradeDocument.DocumentKapali → IsDocumentClosed';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='ED_SonIslemDate')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.ED_SonIslemDate',             'ED_LastProcessDate',        'COLUMN';
    PRINT 'TradeDocument.ED_SonIslemDate → ED_LastProcessDate';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='ToplamVatTutar')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.ToplamVatTutar',              'TotalVatAmount',            'COLUMN';
    PRINT 'TradeDocument.ToplamVatTutar → TotalVatAmount';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='ToplamTutar')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.ToplamTutar',                 'TotalAmount',               'COLUMN';
    PRINT 'TradeDocument.ToplamTutar → TotalAmount';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='ElektronikBelgeNo')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.ElektronikBelgeNo',           'ElectronicDocumentNo',      'COLUMN';
    PRINT 'TradeDocument.ElektronikBelgeNo → ElectronicDocumentNo';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='ElektronikBelgeHatalari')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.ElektronikBelgeHatalari',     'ElectronicDocumentErrors',  'COLUMN';
    PRINT 'TradeDocument.ElektronikBelgeHatalari → ElectronicDocumentErrors';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocument' AND COLUMN_NAME='ElektronikBelgeGonderimStatus')
BEGIN
    EXEC sp_rename 'trade.TradeDocument.ElektronikBelgeGonderimStatus','ElectronicDocumentSendStatus','COLUMN';
    PRINT 'TradeDocument.ElektronikBelgeGonderimStatus → ElectronicDocumentSendStatus';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- trade.TradeDocumentLine
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLine' AND COLUMN_NAME='HareketType')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLine.HareketType',          'DocumentType',          'COLUMN';
    PRINT 'TradeDocumentLine.HareketType → DocumentType';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLine' AND COLUMN_NAME='SeriNo')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLine.SeriNo',               'SerialNo',              'COLUMN';
    PRINT 'TradeDocumentLine.SeriNo → SerialNo';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLine' AND COLUMN_NAME='UnitKatsayi')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLine.UnitKatsayi',          'UnitMultiplier',        'COLUMN';
    PRINT 'TradeDocumentLine.UnitKatsayi → UnitMultiplier';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLine' AND COLUMN_NAME='VatTutari')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLine.VatTutari',            'VatAmount',             'COLUMN';
    PRINT 'TradeDocumentLine.VatTutari → VatAmount';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLine' AND COLUMN_NAME='SatirTutari')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLine.SatirTutari',          'LineAmount',            'COLUMN';
    PRINT 'TradeDocumentLine.SatirTutari → LineAmount';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLine' AND COLUMN_NAME='StokSayimiDahilEtme')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLine.StokSayimiDahilEtme',  'ExcludeFromStockCount', 'COLUMN';
    PRINT 'TradeDocumentLine.StokSayimiDahilEtme → ExcludeFromStockCount';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLine' AND COLUMN_NAME='VadeFarkiPercent')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLine.VadeFarkiPercent',     'DeferralPercent',       'COLUMN';
    PRINT 'TradeDocumentLine.VadeFarkiPercent → DeferralPercent';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- trade.TradeDocumentLineTemp
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLineTemp' AND COLUMN_NAME='SepetId')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLineTemp.SepetId',              'CartId',              'COLUMN';
    PRINT 'TradeDocumentLineTemp.SepetId → CartId';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLineTemp' AND COLUMN_NAME='HareketType')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLineTemp.HareketType',          'DocumentType',        'COLUMN';
    PRINT 'TradeDocumentLineTemp.HareketType → DocumentType';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLineTemp' AND COLUMN_NAME='SeriNo')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLineTemp.SeriNo',               'SerialNo',            'COLUMN';
    PRINT 'TradeDocumentLineTemp.SeriNo → SerialNo';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLineTemp' AND COLUMN_NAME='UnitKatsayi')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLineTemp.UnitKatsayi',          'UnitMultiplier',      'COLUMN';
    PRINT 'TradeDocumentLineTemp.UnitKatsayi → UnitMultiplier';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLineTemp' AND COLUMN_NAME='ToplamTutarVatHaric')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLineTemp.ToplamTutarVatHaric',  'TotalAmountExVat',    'COLUMN';
    PRINT 'TradeDocumentLineTemp.ToplamTutarVatHaric → TotalAmountExVat';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLineTemp' AND COLUMN_NAME='ToplamTutar')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLineTemp.ToplamTutar',          'TotalAmount',         'COLUMN';
    PRINT 'TradeDocumentLineTemp.ToplamTutar → TotalAmount';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLineTemp' AND COLUMN_NAME='TradeDocumentHareketGId')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLineTemp.TradeDocumentHareketGId','TradeDocumentTypeGId','COLUMN';
    PRINT 'TradeDocumentLineTemp.TradeDocumentHareketGId → TradeDocumentTypeGId';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLineTemp' AND COLUMN_NAME='Tukenmis')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLineTemp.Tukenmis',             'IsSoldOut',           'COLUMN';
    PRINT 'TradeDocumentLineTemp.Tukenmis → IsSoldOut';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLineTemp' AND COLUMN_NAME='StoreTanimGId')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLineTemp.StoreTanimGId',        'StoreGId',            'COLUMN';
    PRINT 'TradeDocumentLineTemp.StoreTanimGId → StoreGId';
END
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentLineTemp' AND COLUMN_NAME='VadeFarkiPercent')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentLineTemp.VadeFarkiPercent',     'DeferralPercent',     'COLUMN';
    PRINT 'TradeDocumentLineTemp.VadeFarkiPercent → DeferralPercent';
END
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- trade.TradeDocumentTemp
-- ─────────────────────────────────────────────────────────────────────────────
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='trade' AND TABLE_NAME='TradeDocumentTemp' AND COLUMN_NAME='HareketType')
BEGIN
    EXEC sp_rename 'trade.TradeDocumentTemp.HareketType', 'DocumentType', 'COLUMN';
    PRINT 'TradeDocumentTemp.HareketType → DocumentType';
END
GO

PRINT '=== PATCH 002 tamamlandı ===';
GO
