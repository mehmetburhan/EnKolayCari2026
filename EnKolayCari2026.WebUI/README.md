# EnKolayCari2026.WebUI

## GENERATE UYARISI (Domain / Application.Model DTO / Persistence Config)
Bu cikti EFGenerator urunudur. AI/gelistirici mevcut generate dosyalarini duzenlemez.

## WebUI / .cshtml KESIN KURAL
Generate `*Dto` WebUI view modeli degildir. `.cshtml` icin UIModel/WebModel kullan.

## WebUI URL KESIN KURAL
Grid -> edit/detail URL yalniz **GId (Guid)**. URL icinde **Id (bigint) yasak**.

## Amac
Metronic MVC. HTTP sadece Web.Common uzerinden.

## Bagimliliklar
Web.Common, Language.

## DO
- Katman amacina uygun kod yaz
- Namespace: `EnKolayCari2026.WebUI.*`

## DON'T
- Domain/Persistence referansi. Edit URL'de Id (bigint) yasak; GId kullan.

## AI Kurallari
- AIProjeMimari Volume 01-02 ve playbook (09) kurallarina uy

## Ilgili Dokumanlar
- AIProjeMimari docs/volumes/02-KATMAN-ISIMLERI-VE-PROJE-HARITASI.md
