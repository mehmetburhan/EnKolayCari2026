# Table Mapping — Legacy TR → EnKolayCari EN

| Legacy DB | Legacy Table (TR) | New Schema | New Table (EN) | Notes |
|-----------|-------------------|------------|----------------|-------|
| TICARI_MASTER | `AggregatedCounter` | `HangFire` | `AggregatedCounter` |  |
| TICARI_MASTER | `Counter` | `HangFire` | `Counter` |  |
| TICARI_MASTER | `Hash` | `HangFire` | `Hash` |  |
| TICARI_MASTER | `Job` | `HangFire` | `Job` |  |
| TICARI_MASTER | `JobParameter` | `HangFire` | `JobParameter` |  |
| TICARI_MASTER | `JobQueue` | `HangFire` | `JobQueue` |  |
| TICARI_MASTER | `List` | `HangFire` | `List` |  |
| TICARI_MASTER | `Schema` | `HangFire` | `Schema` |  |
| TICARI_MASTER | `Set` | `HangFire` | `Set` |  |
| TICARI_MASTER | `State` | `HangFire` | `State` |  |
| TICARI_MASTER | `MerkezDoviz` | `common` | `CentralCurrency` |  |
| TICARI_MASTER | `MerkezDovizKur` | `common` | `CentralCurrencyRate` |  |
| TICARI_MASTER | `SirketTanim` | `common` | `Company` |  |
| TICARI_SLAVE1 | `Sayac` | `common` | `Counter` |  |
| TICARI_SLAVE1 | `SayacReferans` | `common` | `CounterReference` |  |
| TICARI_SLAVE1 | `EMailTanim` | `common` | `EmailTemplate` |  |
| TICARI_MASTER | `Roles` | `common` | `LegacyRole` | Migrate to dbo.AspNetRoles |
| TICARI_MASTER | `Lisans` | `common` | `License` |  |
| TICARI_MASTER | `LisansTipi` | `common` | `LicenseType` |  |
| TICARI_MASTER | `SayfaTanim` | `common` | `PageDef` |  |
| TICARI_MASTER | `Token` | `common` | `Token` |  |
| TICARI_SLAVE1 | `CariTanim` | `finance` | `Account` |  |
| TICARI_SLAVE1 | `CariTanimAdres` | `finance` | `AccountAddress` |  |
| TICARI_SLAVE1 | `CariTanimBelge` | `finance` | `AccountDocument` |  |
| TICARI_SLAVE1 | `CariHareket` | `finance` | `AccountTransaction` |  |
| TICARI_SLAVE1 | `KasaTanim` | `finance` | `CashRegister` |  |
| TICARI_SLAVE1 | `KasaPersonel` | `finance` | `CashRegisterPersonal` |  |
| TICARI_SLAVE1 | `KasaHareket` | `finance` | `CashTransaction` |  |
| TICARI_SLAVE1 | `CekSenetTanim` | `finance` | `CheckNote` |  |
| TICARI_SLAVE1 | `CekSenetHareket` | `finance` | `CheckNoteTransaction` |  |
| TICARI_SLAVE1 | `DovizTanim` | `finance` | `Currency` |  |
| TICARI_SLAVE1 | `DovizKur` | `finance` | `CurrencyRate` |  |
| TICARI_SLAVE1 | `MarkaTanim` | `inventory` | `Brand` |  |
| TICARI_SLAVE1 | `UrunTanim` | `inventory` | `Product` |  |
| TICARI_SLAVE1 | `UrunBarkod` | `inventory` | `ProductBarcode` |  |
| TICARI_SLAVE1 | `UrunRenkPaleti` | `inventory` | `ProductColorPalette` |  |
| TICARI_SLAVE1 | `UrunBirim` | `inventory` | `ProductUnit` |  |
| TICARI_SLAVE1 | `SayimTanim` | `inventory` | `StockCount` |  |
| TICARI_SLAVE1 | `SayimHareket` | `inventory` | `StockCountLine` |  |
| TICARI_SLAVE1 | `DepoTanim` | `inventory` | `Store` |  |
| TICARI_SLAVE1 | `DepoPersonel` | `inventory` | `StorePersonal` |  |
| TICARI_SLAVE1 | `BirimTanim` | `inventory` | `Unit` |  |
| TICARI_SLAVE1 | `RaporTanim` | `report` | `ReportDef` |  |
| TICARI_SLAVE1 | `Fatura` | `trade` | `TradeDocument` | Siparis/fatura/teklif ortak baslik |
| TICARI_SLAVE1 | `FaturaDoviz` | `trade` | `TradeDocumentCurrency` |  |
| TICARI_SLAVE1 | `FaturaHareket` | `trade` | `TradeDocumentLine` |  |
| TICARI_SLAVE1 | `FaturaHareketTemp` | `trade` | `TradeDocumentLineTemp` |  |
| TICARI_SLAVE1 | `FaturaTemp` | `trade` | `TradeDocumentTemp` |  |