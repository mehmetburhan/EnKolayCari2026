# Bağlantı bilgileri — EKCN2026

| Alan | Değer |
|------|--------|
| Server | `uygulama.enkolaycari.com` |
| Database | `EKCN2026` |
| User | `memosa` |
| Password | `sql123admin` |
| Collation | `Turkish_CI_AI` |

## Connection string (.NET)

```
Server=uygulama.enkolaycari.com;Database=EKCN2026;User Id=memosa;Password=sql123admin;TrustServerCertificate=True;Encrypt=True;
```

## sqlcmd

```powershell
sqlcmd -S uygulama.enkolaycari.com -U memosa -P "sql123admin" -d EKCN2026 -C
```

## Schema kurulum (sıfırdan)

```powershell
cd C:\Dev\GitHub\EnKolayCari\EnkolayCari2026
python scripts/generate_ddl.py
python scripts/build_full_ddl.py
sqlcmd -S uygulama.enkolaycari.com -U memosa -P "sql123admin" -C -f 65001 -i database\EnKolayCari2026_FullSchema.sql
```

> `FullSchema.sql` mevcut `EKCN2026` veritabanını **DROP + CREATE** eder.

## Adlandırma kuralları

- Tablo ve alan adları: **İngilizce (EN)**
- `MS_Description` açıklamaları: **Türkçe (TR)** + eski alan eşlemesi (`Eski tablo:` / `Eski alan:`)
