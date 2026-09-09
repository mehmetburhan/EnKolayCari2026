# EnKolayCari2026.DataBridge

WPF masaüstü uygulaması: `TICARI_MASTER` + `TICARI_SLAVE1` → `EKCN2026` şirket bazlı veri aktarımı.

## Kapsam (katalog)

Domain entity `Eski alan:` yorumlarından üretilen `Catalog/transfer-catalog.json` — 38 tablo:

- **common:** Company, License*, CentralCurrency*, LegacyRole, PageDef, Token, Counter*, EmailTemplate
- **finance:** Account, AccountTransaction, AccountAddress/Document, CashRegister*, CashTransaction, CheckNote*, Currency*
- **inventory:** Product, ProductBarcode/Unit/ColorPalette, Brand, Unit, Store*, StockCount*
- **trade:** TradeDocument, TradeDocumentLine, TradeDocumentCurrency, Temp*
- **report:** ReportDef

HangFire şirket transferine **dahil değil**.

## Çalışma

1. Bağlantıları kontrol et (appsettings.json)
2. Şirketleri yükle → seç
3. Önizleme (satır sayıları)
4. Aktarımı başlat

Idempotent: hedefte aynı `GId` varsa skip; `common.LegacyTransferMap` güncellenir.

## Katalog yenileme

```powershell
python scripts/gen_transfer_catalog.py
```
