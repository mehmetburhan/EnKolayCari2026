# EnKolayCari2026 — Final Schema & Table List

| Alan | Değer |
|------|--------|
| Tarih | 2026-09-04 |
| Durum | Aktif hedef şema (ecommerce / integration / content / platform hariç) |
| Kaynak | AIProjeMimari platform (`EKCN2026`) + legacy MASTER/SLAVE eşlemesi |
| Sunucu | `uygulama.enkolaycari.com` / DB `EKCN2026` |
| Adlandırma | Tablo/alan EN · MS_Description TR |

Bu dosya konuşma boyunca netleşen **son şema/tablo listesinin** kalıcı kaydıdır.  
Detay: `SCHEMA-PLAN.md`, `TABLE-MAPPING.md`, `MIGRATION-NOTES.md` (§25 LegacyTransferMap), `COLUMN-NAMING.md`, `CONNECTION.md`.

---

## Aktif şemalar (7)

`common` · `dbo` · `finance` · `inventory` · `trade` · `report` · `HangFire`

---

### `common`

| Yeni | Eski (legacy) |
|------|----------------|
| Company | SirketTanim |
| Branch | — *(AI yeni)* |
| Personal | PersonelTanim |
| PersonalBranch | — *(AI yeni)* |
| PersonalCompany | — *(AI yeni)* |
| PersonalGroup | — *(AI yeni)* |
| PersonalGroupDef | — *(AI yeni)* |
| PersonalLoginActivity | — *(AI yeni)* |
| PersonalWidget | — *(AI yeni)* |
| License | Lisans |
| LicenseType | LisansTipi |
| Token | Token |
| PageDef | SayfaTanim |
| LegacyRole | Roles |
| City | Iller + Ilceler |
| Country | — *(AI yeni)* |
| CountryHolidays | — *(AI yeni)* |
| CodeDef | — *(AI yeni)* |
| Languages | — *(AI yeni)* |
| TranslationDef | — *(AI yeni)* |
| CentralCurrency | MerkezDoviz |
| CentralCurrencyRate | MerkezDovizKur |
| Counter | Sayac |
| CounterReference | SayacReferans |
| FileHeader | Dosyalar |
| FileBlob | DosyalarBlob |
| EmailTemplate | EMailTanim |
| LabelDef | EtiketTanim |
| LabelPool | EtiketHavuzu |
| GroupDef | — *(AI yeni)* |
| GroupDefCountry | — *(AI yeni)* |
| Moduls | Modul |
| ProcessType | — *(AI yeni)* |
| ProcessFlow | — *(AI yeni)* |
| ProcessTypePersonal | — *(AI yeni)* |
| AppSettings | — *(AI yeni)* |
| AppExceptionLog | — *(AI yeni)* |
| MailLog | — *(AI yeni)* |
| LegacyTransferMap | — *(transfer/analiz; kayit esleme)* |

### `dbo`

| Yeni | Eski (legacy) |
|------|----------------|
| AspNetUsers | Identity *(PersonelTanim.UserId)* |
| AspNetRoles | Roles |
| AspNetUserRoles | UserRoles *(migrate ertelendi)* |
| AspNetUserClaims | — *(Identity)* |
| AspNetRoleClaims | — *(Identity)* |
| AspNetUserLogins | — *(Identity)* |
| AspNetUserTokens | — *(Identity)* |
| MenuRole | SayfaTanim + Roles |
| sysdiagrams | sysdiagrams |

### `finance`

| Yeni | Eski |
|------|------|
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

### `inventory`

| Yeni | Eski |
|------|------|
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

### `trade`

| Yeni | Eski |
|------|------|
| TradeDocument | Fatura *(sipariş / fatura / teklif — tip alanı ile)* |
| TradeDocumentLine | FaturaHareket |
| TradeDocumentCurrency | FaturaDoviz |
| TradeDocumentTemp | FaturaTemp |
| TradeDocumentLineTemp | FaturaHareketTemp |

### `report`

| Yeni | Eski |
|------|------|
| ReportDef | RaporTanim |

### `HangFire`

| Yeni | Eski |
|------|------|
| AggregatedCounter | AggregatedCounter |
| Counter | Counter |
| Hash | Hash |
| Job | Job |
| JobParameter | JobParameter |
| JobQueue | JobQueue |
| List | List |
| Schema | Schema |
| Server | Server *(HangFire)* |
| Set | Set |
| State | State |

---

## Kapsam dışı (şimdilik)

### Şemalar
| Şema | Örnek legacy |
|------|----------------|
| `ecommerce` | ETicaret* |
| `integration` | Pazaryeri*, EntegrasyonNetsis, UrunPazaryeriOzellik |
| `content` | Makale*, SeoTanim, BelgeTanim |
| `platform` | Logs, BinList, TmpFace, FieldTrToEn, ServerTanim, Server |

### Tablolar (aktif şemalardan hariç)
| Hariç yeni ad | Legacy |
|---------------|--------|
| ProductRecipe / ProductRecipeLine | UrunReceteTanim / UrunReceteHareket |
| ProductReview | UrunYorum |
| ProductPriceGroup | UrunFiyatGrup |
| Category / CategoryDetail | KategoriTanim / KategoriDetay |
| PriceGroup | FiyatGrupTanim |
| AttributeDef / AttributeDetail | OzellikTanim / OzellikDetay |
| EDocument / EDocumentLine | EBelge / EBelgeHareket |
| Event / EventParticipant | Etkinlik / EtkinlikKatilimci |

---

## Önemli adlandırma kararları

| Karar | Açıklama |
|-------|----------|
| `trade.TradeDocument` | Eski `Fatura`; sipariş + fatura + teklif ortak başlık |
| `inventory.Store` | Eski `DepoTanim` (`Warehouse` kullanılmaz) |
| `StoreId` | Eski `DepoTanimId` |
| `TradeDocumentId` | Eski `FaturaId` |
| `TradeDocumentLineId` | Eski `FaturaHareketId` |
| Etiket | `common.LabelDef` / `LabelPool` (content şeması yok) |

## Tenant / audit

- İş tabloları: `CompanyId` (eski `SirketTanimId`), audit **Family B** (`Insert*` / `Update*` / `Delete*`)
- AI platform (`Personal`, `Branch`, …): `CompanyMasterCode` veya `CompanyId`, audit **Family A** (`Created*` / `Modifed*`)
- URL / API dış kimlik: **GId**; `Id` (bigint) URL’de yok
