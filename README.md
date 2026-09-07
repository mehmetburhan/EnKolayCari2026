# EnKolayCari — Unified Database

Master (`TICARI_MASTER`) ve Slave (`TICARI_SLAVE1`) veritabanlarının tek SQL Server DB'de birleştirilmiş hali.

## Hedefler

- Tablo ve kolon adları **İngilizce (EN)**
- `MS_Description` açıklamaları **Türkçe (TR)** (+ eski TR tablo/alan eşlemesi)
- Tablolar schema altında gruplanır
- Eski ↔ yeni eşleme: `docs/TABLE-MAPPING.md`

## Bağlantı

| | |
|--|--|
| Server | `uygulama.enkolaycari.com` |
| Database | **EKCN2026** |
| User | `memosa` |
| Password | `sql123admin` |

Detay: [docs/CONNECTION.md](docs/CONNECTION.md)

## Klasör yapısı

```
EnkolayCari2026/
├── README.md
├── docs/
│   ├── CONNECTION.md        # Sunucu bağlantı bilgileri
│   ├── ANALYSIS-LegacyTransferMap.md  # Transfer esleme analizi
│   ├── SCHEMA-PLAN.md
│   ├── FINAL-SCHEMA-LIST.md
│   ├── TABLE-MAPPING.md
│   ├── MIGRATION-NOTES.md
│   └── COLUMN-NAMING.md
├── database/
│   ├── EnKolayCari_CreateDatabase.sql
│   ├── EnKolayCari_FullSchema.sql   # Tam DDL (DROP+CREATE EKCN2026)
│   ├── patches/
│   │   └── 001_LegacyTransferMap.sql   # Mevcut DB'ye incremental
│   └── mappings/tables.json
└── scripts/
    ├── generate_ddl.py
    └── build_full_ddl.py
```

## Veritabanını sıfırdan oluşturma

```powershell
cd C:\Dev\GitHub\EnKolayCari\EnkolayCari2026
python scripts/generate_ddl.py
python scripts/build_full_ddl.py
sqlcmd -S uygulama.enkolaycari.com -U memosa -P "sql123admin" -C -f 65001 -i database\EnKolayCari_FullSchema.sql
```

## Schema özeti (aktif)

`common`, `dbo`, `finance`, `inventory`, `trade`, `report`, `HangFire`

Ertelenen: `ecommerce`, `integration`, `content`, `platform`
