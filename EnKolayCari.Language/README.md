# EnKolayCari.Language

## GENERATE UYARISI (Domain / Application.Model DTO / Persistence Config)
Bu cikti EFGenerator urunudur. AI/gelistirici mevcut generate dosyalarini duzenlemez.

## WebUI / .cshtml KESIN KURAL
Generate `*Dto` WebUI view modeli degildir. `.cshtml` icin UIModel/WebModel kullan.

## WebUI URL KESIN KURAL
Grid -> edit/detail URL yalniz **GId (Guid)**. URL icinde **Id (bigint) yasak**.

## Amac
Lang.resx localization (tr-TR master + en-US).

## Bagimliliklar
Services.Model.

## DO
- Katman amacina uygun kod yaz
- Namespace: `EnKolayCari.Language.*`

## DON'T
- Key'leri sadece UI ile sinirli tutma; servis Message da ayni.

## AI Kurallari
- AIProjeMimari Volume 01-02 ve playbook (09) kurallarina uy

## Ilgili Dokumanlar
- AIProjeMimari docs/volumes/02-KATMAN-ISIMLERI-VE-PROJE-HARITASI.md
