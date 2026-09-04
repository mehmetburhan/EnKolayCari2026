# Migration Notes — EnKolayCari2026

## Kararlar

### 1. Tek veritabanı
Master-Slave ayrımı kaldırıldı. Tüm tenant verisi `CompanyId` ile ayrılır.

### 2. Id tipi: int → bigint
Legacy tablolarda `Id` ve FK alanları `bigint` olarak yeniden tanımlandı. Migration sırasında veri kaybı olmaz; yeni insert'ler daha geniş aralık kullanır.

### 3. Audit Family B korundu
EnKolayCari legacy:
- `InsertUser`, `InsertDateTime`, `RecDateTime`
- `UpdateUser`, `UpdateDateTime`
- `DeleteUser`, `DeleteDateTime`

Auditness Family A (`CreatedDate`, `ModifedDate`) bu projede **kullanılmadı** — mevcut uygulama kodu ile uyum için.

### 4. Stat vs Aktif
`Aktif` (bit) → `Stat` (bit). Auditness convention ile hizalı; değer anlamı aynı (1=aktif).

### 5. Identity birleşimi
| Legacy | Yeni |
|--------|------|
| MASTER.Roles | dbo.AspNetRoles (+ common.LegacyRole geçici) |
| SLAVE.UserRoles | (şimdilik yok — sonra planlanacak; hedef `dbo.AspNetUserRoles`) |
| PersonelTanim.UserId | dbo.AspNetUsers.Id |

### 6. ServerTanim / Server
Unified DB'de slave routing gerekmez. `platform` şeması şimdilik yok; `ServerTanim` / `Server` tabloları oluşturulmaz. `Company.LegacyServerId` varsa FK tanımlanmaz.

### 7. HangFire
MASTER'daki HangFire tabloları standart şema ile oluşturulur. Job queue tek DB üzerinde çalışır.

### 8. sysdiagrams
Unified DB'de `dbo.sysdiagrams` **oluşturulmaz** (SSMS diyagram tablosu hariç tutuldu).

### 9. MS_Description formatı (TR)
Tablo ve alan **adları EN**; açıklamalar **TR**.

Her kolon:
```
{TR açıklama} | Eski alan: {EskiTablo}.{EskiKolon}
```

Her tablo:
```
Eski tablo: {EskiTablo} ({KaynakDB}). Yeni şema: {schema}.{tablo}
```

### 10. Tablo eşleme kaydı
Tablo düzeyi eşleme: `docs/TABLE-MAPPING.md` + `database/mappings/tables.json`.

**Kayıt düzeyi transfer eşlemesi:** `common.LegacyTransferMap` (aşağıda §25).

## Bağlantı

| | |
|--|--|
| Server | `uygulama.enkolaycari.com` |
| Database | `EKCN2026` |
| User | `memosa` |
| Password | `sql123admin` |

Bkz. `docs/CONNECTION.md`

## Migration sırası (önerilen)

1. `EnKolayCari2026_FullSchema.sql` çalıştır (DB: **EKCN2026**, DROP+CREATE)  
   *veya* mevcut DB'ye patch: `database/patches/001_LegacyTransferMap.sql`
2. MASTER referans tablolarını kopyala; her aktarılan satır için `LegacyTransferMap` doldur
3. SLAVE iş tablolarını kopyala (`CompanyId` = eski `SirketTanimId`); eşlemeyi yaz
4. Identity kullanıcı/rol migrate et
5. FK constraint'leri enable et (NOCHECK ile yükleme yapıldıysa)
6. Sequence/identity değerlerini `DBCC CHECKIDENT` ile hizala
7. Uygulama connection string'lerini tek DB'ye yönlendir

### 11. Medical modül hariç
Aşağıdaki legacy tablolar unified DB kapsamı **dışında** bırakıldı (medical modül yok):

- `ICD10`, `HastaAnamnez`, `HastaAnamnezKontrol`, `HastaAnamnezPersonel`
- `Randevu`, `Servis`, `Survey`, `SurveyAnswers`, `QBank`, `QBankOptions`

### 12. Company — kredi kartı / sanal POS alanları hariç
`common.Company` (eski `SirketTanim`) tablosundan banka sanal POS ve kredi kartı ödeme alanları çıkarılmıştır:

- Garanti, Yapı Kredi, VakıfBank, Akbank, İş Bankası, Finansbank, DenizBank, Iyzico, Ziraat, Param POS kimlik bilgileri
- `KrediAktiOdemeAktif`, `KartMaxCekimSayisi`, `TaksitMinimumTutar`, `KartVadeFarkiYasit`

Havale (`BankaHavalesiAktif`) ve kapıda ödeme (`KapidaOdemeAktif`) alanları korunur.

### 13. Conversation tablosu hariç
`Conversation` (SLAVE) unified DB kapsamına alınmadı; `common.Conversation` oluşturulmaz.

### 14. Iller / Ilceler → common.City (AI mimari)
`TICARI_MASTER.dbo.Iller` ve `Ilceler` legacy DDL'den üretilmez.
AIProjeMimari / Auditness kuralı ile `common.City` kullanılır:

| Level | Anlam | Legacy karşılık |
|-------|-------|-----------------|
| 1 | Province (İl) | `Iller` |
| 2 | District (İlçe) | `Ilceler` |

Alanlar: `FullCityCode`, `CountryCode`, `CityCode`, `ParentCityCode`, `CityName`, `Level`.

### 15. EmailQueue tablosu hariç
`EMailKuyruk` (SLAVE) unified DB kapsamına alınmadı; `common.EmailQueue` oluşturulmaz.

### 16. Dosyalar / DosyalarBlob → FileHeader / FileBlob (AI mimari)
`TICARI_SLAVE1.dbo.Dosyalar` ve `DosyalarBlob` legacy DDL'den üretilmez.
AIProjeMimari / Auditness kuralı ile:

| Yeni tablo | Yapı | Legacy karşılık |
|------------|------|-----------------|
| `common.FileHeader` | `TableName`, `TableId`, `FileName`, `FileOrder`, `FileType` | `Dosyalar` |
| `common.FileBlob` | `FileHeaderId`, `Blob` (+ CASCADE FK) | `DosyalarBlob` |

Legacy ek alanlar (`CompanyId`, `AnaResim`, `Prefix`, `W`/`H`, `Satir1..5`, audit kolonları vb.) AI mimari tablosuna taşınmaz; gerekirse ayrı ürün tablosu/extension ile çözülür.

### 17. LegacyUserRole tablosu hariç
`UserRoles` (SLAVE) şimdilik unified DB'ye alınmadı; `common.LegacyUserRole` oluşturulmaz. AspNet Identity rol eşlemesi sonra planlanacak.

### 18. ModulePrice tablosu hariç
`ModulFiyat` (MASTER) unified DB kapsamına alınmadı; `common.ModulePrice` oluşturulmaz.

### 19. PersonelTanim → common.Personal (AI mimari)
`TICARI_MASTER.dbo.PersonelTanim` legacy DDL'den üretilmez.
AIProjeMimari / Auditness `common.Personal` kullanılır:

- Tenant: `CompanyMasterCode` (eski `SirketTanimId` yerine)
- Audit: **Family A** (`CreatedDate`, `CreatedUser`, `ModifedDate`, `ModifedUser`, `DeletedDate`, `DeletedUser`, `IsDelete`)
- Identity: `UserId` / `UserIdForDomain` → `dbo.AspNetUsers`
- Ad/soyad: `Name` + `SurName` (eski tek alan `AdSoyad` migrate edilirken bölünür)

Legacy-only alanlar (tıbbi personel, randevu slot, menü tipi vb.) AI mimari tablosuna taşınmaz.

### 20. SmsLog tablosu hariç
`Sms` (MASTER) unified DB kapsamına alınmadı; `common.SmsLog` oluşturulmaz.

### 21. ecommerce / integration / content / platform şemaları hariç (şimdilik)
Aşağıdaki şemalar ve tablolar unified DB kapsamı **dışında** (sonraki faz):

| Şema | Hariç tutulan legacy tablolar |
|------|-------------------------------|
| `ecommerce` | `ETicaretAyarlar`, `ETicaretCSS`, `ETicaretEvent`, `ETicaretGezilenUrunler`, `ETicaretMenu`, `ETicaretParametre`, `ETicaretPopup`, `ETicaretSlider`, `ETicaretThema`, `ETicaretThemaItem`, `ETicaretUrunParametre` |
| `integration` | `PazaryeriKategori`, `PazaryeriMarka`, `PazaryeriOzellik`, `PazaryeriOzellikDeger`, `PazaryeriLog`, `EntegrasyonNetsis`, `UrunPazaryeriOzellik` |
| `content` | `Makale`, `MakaleParagraf`, `MakaleOneri`, `SeoTanim`, `BelgeTanim` |
| `platform` | `Logs`, `BinList`, `TmpFace`, `FieldTrToEn`, `ServerTanim`, `Server` |

Notlar:
- `EtiketTanim` / `EtiketHavuzu` → AI platform `common.LabelDef` / `LabelPool` (ayrı content.Tag* yok)
- `BelgeTanim` yok; `AccountDocument.DocumentDefId` FK’siz bigint kalabilir

### 22. Fatura → trade.TradeDocument
`Fatura` / `FaturaHareket` tabloları `invoice.Invoice*` yerine `trade.TradeDocument` / `TradeDocumentLine` olarak adlandırılır (sipariş, fatura, teklif ortak başlık). Şema adı: `trade`. FK kolonları: `FaturaId` → `TradeDocumentId`, `FaturaHareketId` → `TradeDocumentLineId`.

### 23. Inventory tabloları hariç (şimdilik)
Aşağıdaki legacy tablolar unified DB kapsamı **dışında**:

- `UrunReceteTanim` / `UrunReceteHareket` → ProductRecipe*
- `UrunYorum` → ProductReview
- `KategoriTanim` / `KategoriDetay` → Category*
- `FiyatGrupTanim` → PriceGroup
- `OzellikTanim` / `OzellikDetay` → Attribute*
- `UrunFiyatGrup` → ProductPriceGroup

### 24. Warehouse → Store; EDocument / Event hariç
- `DepoTanim` / `DepoPersonel` → `inventory.Store` / `StorePersonal` (`Warehouse` kullanılmaz)
- `DepoTanimId` → `StoreId`
- `EBelge` / `EBelgeHareket` → oluşturulmaz
- `Etkinlik` / `EtkinlikKatilimci` → oluşturulmaz

### 25. Analiz — `common.LegacyTransferMap` (kayıt düzeyi transfer eşlemesi)

**Amaç:** MASTER/SLAVE → `EKCN2026` veri aktarımında her satırın *eski kimliği* ile *yeni kimliğini* saklamak; yeniden çalıştırma (idempotent), delta sync ve doğrulama için.

**Neden ayrı tablo?**
- `TABLE-MAPPING.md` / `tables.json` sadece **tablo adı** eşler (CariTanim → Account).
- Transfer sırasında ihtiyaç: **satır** eşlemesi (eski GId → yeni GId + CompanyId).
- Soft-delete / güncelleme farkını yakalamak için eski kayıttaki son hareket tarihi gerekir.

**Alanlar (EN ad / TR anlam):**

| Alan | Anlam |
|------|--------|
| `LegacyTableName` | Eski tablo adı (`CariTanim`, `UrunTanim`, …) |
| `LegacyGId` | Eski satır `GId` |
| `LegacyLastChangeDate` | Eski satırda `InsertDateTime` / `UpdateDateTime` / `DeleteDateTime` içinden **en güncel** olan |
| `NewTableName` | Yeni tablo (`finance.Account`, `inventory.Product`, …) |
| `NewGId` | Yeni satır `GId` |
| `CompanyId` | Yeni kaydın tenant’ı |
| `InsertDateTime` | Eşleme satırının yazıldığı an |

**`LegacyLastChangeDate` hesabı (kaynak):**
```sql
(SELECT MAX(d) FROM (VALUES (InsertDateTime), (UpdateDateTime), (DeleteDateTime)) AS X(d))
```

**Kullanım senaryoları:**
1. İlk yükleme: insert sonrası map satırı yaz → aynı `LegacyGId` tekrar gelirse skip/update.
2. Delta: kaynakta `LegacyLastChangeDate` map’tekinden yeni ise yeniden aktar.
3. Doğrulama: map sayısı ≈ kaynak satır; orphan / eksik GId raporu.
4. FK çözümü: eski `CariTanimId` yerine map üzerinden yeni Account `GId`/`Id` bul.

**Index:**
- Unique: `(LegacyTableName, LegacyGId)` — bir eski satır bir kez eşlenir
- Lookup: `(CompanyId, NewTableName, NewGId)`
- Delta: `(LegacyLastChangeDate)`

**DDL:** `database/patches/001_LegacyTransferMap.sql` (mevcut DB) · FullSchema / CreateDatabase generator’a gömülü.

**Not:** `platform.LegacyTableMapping` yok; bu tablo `common` altında ve kayıt odaklıdır.

## Bilinen farklar

- View'lar ve function'lar henüz port edilmedi (Auditness örneğindeki `FN_Get*` vb.)
- Stored procedure'ler taşınmadı
- Index'ler generator'da minimal; production öncesi performans review gerekir
- FK'ler generator'da yalnızca bilinen ana referanslar için eklendi (CompanyId, PersonalId, AccountId, …)

## Rollback

Migration öncesi MASTER ve SLAVE DB backup alın. Unified DB ayrı instance/database olarak oluşturulduğu için legacy sistem etkilenmez.
