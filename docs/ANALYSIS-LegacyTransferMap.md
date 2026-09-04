# Analiz — common.LegacyTransferMap

| | |
|--|--|
| Tarih | 2026-09-04 |
| DB | `EKCN2026` @ `uygulama.enkolaycari.com` |
| Şema | `common` |
| Patch | `database/patches/001_LegacyTransferMap.sql` |

## Problem

Tablo eşlemesi (`TABLE-MAPPING.md`) yeterli değil. Transfer sırasında her **satır** için:

1. Eski tablonun adı  
2. Eski satır `GId`  
3. Eski satırın son hareket tarihi (`Insert` / `Update` / `Delete` içinden en güncel)  
4. Yeni tablo adı  
5. Yeni satır `GId`  
6. Yeni `CompanyId`  

bilinmeli. Aksi halde yeniden koşum, delta sync ve FK çözümlemesi güvenilir olmaz.

## Çözüm

`common.LegacyTransferMap` — kayıt düzeyi eşleme tablosu.

```
LegacyTableName + LegacyGId  →  NewTableName + NewGId + CompanyId
                                  (+ LegacyLastChangeDate)
```

## Alanlar

| Alan (EN) | Açıklama (TR) |
|-----------|----------------|
| Id | PK, Identity |
| GId | Eşleme satırının Guid’i |
| CompanyId | Yeni kaydın tenant Id’si |
| LegacyTableName | Eski tablo (`CariTanim`, …) |
| LegacyGId | Eski satır GId |
| LegacyLastChangeDate | Insert/Update/Delete max tarihi |
| NewTableName | Yeni tablo (`finance.Account`, …) |
| NewGId | Yeni satır GId |
| InsertDateTime | Eşlemenin yazıldığı an |

## Son hareket tarihi

```sql
LegacyLastChangeDate = (
  SELECT MAX(d) FROM (VALUES
    (InsertDateTime),
    (UpdateDateTime),
    (DeleteDateTime)
  ) AS X(d)
)
```

## Index stratejisi

| Index | Amaç |
|-------|------|
| `UQ_LegacyTransferMap_LegacyTable_LegacyGId` | Aynı eski satır tek eşleme |
| `IX_…_CompanyId_NewTable_NewGId` | Yeni taraftan geri arama |
| `IX_…_LegacyLastChangeDate` | Delta / incremental sync |

## İlişki

- Tablo düzeyi sözlük: `docs/TABLE-MAPPING.md` + `mappings/tables.json`  
- Satır düzeyi sözlük: **bu tablo**  
- `platform.LegacyTableMapping` **yok** (platform şeması erteli)

## Referanslar

- `docs/MIGRATION-NOTES.md` §25  
- `docs/FINAL-SCHEMA-LIST.md` (`common.LegacyTransferMap`)  
- `docs/SCHEMA-PLAN.md`
