# Schema Plan — EnKolayCari

## Tasarım prensipleri

1. **Domain-driven schemas** — Auditness / AIProjeMimari ile uyumlu
2. **EN isimlendirme** — tablo ve kolon; açıklamada legacy TR referansı
3. **Tek tenant kolonu** — `CompanyId` (eski `SirketTanimId`); platform Personal/Branch → `CompanyMasterCode`
4. **Soft delete** — Family B (`DeleteDateTime` / `DeleteUser`) veya Family A (`IsDelete` + `Deleted*`) tabloya göre
5. **HangFire + Identity** — `HangFire` ve `dbo` şemaları standart

## Aktif şemalar

### common
Çapraz modül referans verileri ve paylaşılan entity'ler.

| Yeni tablo | Eski tablo | Kaynak |
|------------|------------|--------|
| Company | SirketTanim | MASTER |
| Personal | PersonelTanim | AI mimari (Family A, CompanyMasterCode) |
| Branch | — | AI mimari |
| License | Lisans | MASTER |
| LicenseType | LisansTipi | MASTER |
| Moduls | Modul | MASTER |
| Token | Token | MASTER |
| PageDef | SayfaTanim | MASTER |
| City | Iller + Ilceler | AI mimari (`Level` 1/2) |
| CentralCurrency | MerkezDoviz | MASTER |
| CentralCurrencyRate | MerkezDovizKur | MASTER |
| Counter | Sayac | SLAVE |
| CounterReference | SayacReferans | SLAVE |
| FileHeader | Dosyalar | AI mimari (`TableName` + `TableId`) |
| FileBlob | DosyalarBlob | AI mimari (`FileHeaderId`) |
| EmailTemplate | EMailTanim | SLAVE |
| LegacyRole | Roles | MASTER |
| LabelDef | EtiketTanim | AI mimari |
| LabelPool | EtiketHavuzu | AI mimari |
| LegacyTransferMap | — | Transfer kayit esleme (analiz) |
| CodeDef | — | AI platform |
| Country | — | AI platform |
| CountryHolidays | — | AI platform |
| Languages | — | AI platform |
| TranslationDef | — | AI platform |
| GroupDef | — | AI platform |
| GroupDefCountry | — | AI platform |
| ProcessType | — | AI platform |
| ProcessFlow | — | AI platform |
| ProcessTypePersonal | — | AI platform |
| AppSettings | — | AI platform |
| AppExceptionLog | — | AI platform |
| MailLog | — | AI platform |
| PersonalBranch | — | AI platform |
| PersonalCompany | — | AI platform |
| PersonalGroup | — | AI platform |
| PersonalGroupDef | — | AI platform |
| PersonalLoginActivity | — | AI platform |
| PersonalWidget | — | AI platform |

Tam liste: [FINAL-SCHEMA-LIST.md](FINAL-SCHEMA-LIST.md).

### finance
Cari, kasa, çek/senet, döviz.

| Yeni tablo | Eski tablo |
|------------|------------|
| Account | CariTanim |
| AccountTransaction | CariHareket |
| AccountAddress | CariTanimAdres |
| AccountDocument | CariTanimBelge |
| CashRegister | KasaTanim |
| CashTransaction | KasaHareket |
| CashRegisterPersonal | KasaPersonel |
| CheckNote | CekSenetTanim |
| CheckNoteTransaction | CekSenetHareket |
| Currency | DovizTanim |
| CurrencyRate | DovizKur |

### inventory
Ürün, depo (Store), sayım, marka.

| Yeni tablo | Eski tablo |
|------------|------------|
| Product | UrunTanim |
| ProductBarcode | UrunBarkod |
| ProductUnit | UrunBirim |
| ProductColorPalette | UrunRenkPaleti |
| Brand | MarkaTanim |
| Unit | BirimTanim |
| Store | DepoTanim |
| StorePersonal | DepoPersonel |
| StockCount | SayimTanim |
| StockCountLine | SayimHareket |

> Hariç (şimdilik): ProductRecipe*, ProductReview, ProductPriceGroup, Category*, PriceGroup, Attribute*

### trade
Sipariş / fatura / teklif ortak ticari belge.

| Yeni tablo | Eski tablo |
|------------|------------|
| TradeDocument | Fatura *(tip ile sipariş/fatura/teklif)* |
| TradeDocumentLine | FaturaHareket |
| TradeDocumentCurrency | FaturaDoviz |
| TradeDocumentTemp | FaturaTemp |
| TradeDocumentLineTemp | FaturaHareketTemp |

> Hariç: EDocument / EDocumentLine (EBelge*)

### report
Rapor tanımları.

| Yeni tablo | Eski tablo |
|------------|------------|
| ReportDef | RaporTanim |

> Hariç: Event / EventParticipant (Etkinlik*)

### dbo
ASP.NET Identity + menü rol.

- AspNetRoles, AspNetUsers, AspNetUserRoles, AspNetUserClaims, AspNetRoleClaims, AspNetUserLogins, AspNetUserTokens
- MenuRole
- sysdiagrams

### HangFire
Standart Hangfire 1.x tabloları (değiştirilmez).

## Şimdilik kapsam dışı şemalar

Aşağıdaki şemalar ve tablolar **oluşturulmaz** (sonraki faz):

| Şema | Legacy örnekleri |
|------|------------------|
| `ecommerce` | ETicaret* |
| `integration` | Pazaryeri*, EntegrasyonNetsis, UrunPazaryeriOzellik |
| `content` | Makale*, SeoTanim, BelgeTanim (etiket → `common.Label*`) |
| `platform` | Logs, BinList, TmpFace, FieldTrToEn, ServerTanim, Server, LegacyTableMapping |

Eski ↔ yeni tablo eşlemesi dokümanda tutulur: `docs/TABLE-MAPPING.md` (DB tablosu yok).

## İlişki notları

- Tüm SLAVE tablolarındaki `SirketTanimId` → `CompanyId` (`common.Company.Id`)
- `PersonelTanimId` → `PersonalId` (`common.Personal.Id`)
- `CariTanimId` → `AccountId` (`finance.Account.Id`)
- `ServerId` / `LegacyServerId` — platform şeması olmadığı için FK yok; kolon gerekirse nullable tutulur

## Sonraki adımlar

1. Data migration scriptleri (MASTER + SLAVE → unified; yalnızca aktif şemalar)
2. EF Core DbContext schema mapping
3. View ve stored procedure port (legacy SP'ler henüz taşınmadı)
4. İhtiyaç halinde ecommerce / integration / content / platform fazı
