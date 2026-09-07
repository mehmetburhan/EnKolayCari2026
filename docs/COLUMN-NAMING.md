# Column Naming Rules — EnKolayCari

## Genel kurallar

1. PascalCase **İngilizce** — Türkçe alan adı bırakılmaz (`EMukellefAktif` → `IsETaxpayerActive`)
2. FK alanları: `{Entity}Id` (ör. `CompanyId`, `ProductId`)
3. Legacy TR adı ve açıklama `MS_Description` içinde **Türkçe** saklanır (`Eski alan: ...`)
4. Marka/ürün özel adları korunabilir (`TrendyolId`) ama Türkçe ekler çevrilir (`TrendyolSaticiId` → `TrendyolSellerId`)
5. `Aktif` tek başına → `Stat`; bileşiklerde → `Active` / `Is…Active`

## Sabit eşlemeler

| Legacy (TR) | New (EN) | Not |
|-------------|----------|-----|
| SirketTanimId | CompanyId | Tenant FK |
| PersonelTanimId | PersonalId | |
| CariTanimId | AccountId | |
| UrunTanimId | ProductId | |
| FaturaId | TradeDocumentId | Sipariş/fatura/teklif başlık FK |
| FaturaHareketId | TradeDocumentLineId | |
| DepoTanimId | StoreId | |
| KasaTanimId | CashRegisterId | |
| Aktif | Stat | 1=aktif (tek alan) |
| EMukellefAktif | IsETaxpayerActive | |
| IskontoOrani | DiscountRate | |
| KayitYeri | RecordSource | 0=Normal, 1=ETicaret |
| Barkod | Barcode | |
| SatisFiyati | SalePrice | |
| AlisFiyati | PurchasePrice | |
| RenkBeden | ColorSize | |
| DepoId | StoreId | |
| Kod | Code | |
| Unvan | Title | |
| Ad | FirstName | |
| Soyad | LastName | |
| Aciklama | Description | |
| Tarih | TransactionDate | hareket tablolarında |
| Tutar | Amount | |
| Borc | DebitAmount | |
| Alacak | CreditAmount | |
| InsertUser | InsertUser | Family B — korunur |
| InsertDateTime | InsertDateTime | |
| UpdateUser | UpdateUser | |
| UpdateDateTime | UpdateDateTime | |
| DeleteUser | DeleteUser | |
| DeleteDateTime | DeleteDateTime | |

## Prefix çevirileri

| TR Prefix | EN Prefix |
|-----------|-----------|
| Sirket | Company |
| Personel | Personal |
| Cari | Account |
| Urun | Product |
| Fatura | TradeDocument |
| Kasa | CashRegister |
| Depo | Store |
| Doviz | Currency |
| Ozellik | Attribute |
| Sayim | StockCount |
| Etiket | Label |

> `Etiket` → AI `LabelDef`/`LabelPool` (`Tag` değil).  
> Ertelenen: `Pazaryeri`→Marketplace, `ETicaret`→ECommerce, `Makale`→Article (ecommerce/integration/content fazı).

## Suffix çevirileri

| TR Suffix | EN Suffix |
|-----------|-----------|
| TanimId | DefId |
| Tanim | Def |
| Hareket | Transaction |
| Kodu | Code |
| Tarihi | Date |
| Orani | Rate |
| Tipi | Type |

## Özel alanlar

POS, banka entegrasyon ve e-ticaret alanları (Garanti*, Iyzico*, Trendyol* vb.) anlam kaybı olmaması için **orijinal isimle** veya sadece prefix normalize edilerek bırakılır. Generator `translate_column()` fonksiyonu bilinen İngilizce isimleri korur.

## Generator

Kolon eşlemesi `scripts/generate_ddl.py` içindeki `COLUMN_MAP`, `SUFFIX_RULES` ve `prefix_map` ile yapılır. Manuel override gerekirse `tables.json` yanına `columns.json` eklenebilir (gelecek sürüm).
