# EnKolayCari2026.BlazorUI

## Amac
`EnKolayCari2026.WebUI` (Metronic MVC) icin alternatif bir sunum katmani. Blazor Web App, Interactive Server render modu ile calisir. HTTP sadece Web.Common uzerinden.

## Bagimliliklar
Web.Common, Language.

## DO
- Katman amacina uygun kod yaz
- Namespace: `EnKolayCari2026.BlazorUI.*`
- Sayfa basligi/alt basligi icin `<PageHeading Title="..." Subtitle="..." />` bileseni kullan (Components/Layout/PageHeading.razor)

## DON'T
- Domain/Persistence referansi

## Styling (Tailwind CSS)
- WebUI ile ayni tasarim dili (nexora tema): sidebar + header dashboard layout.
- Kaynak: `Styles/app.css`. Uretilen dosya: `wwwroot/app.css` (elle duzenlenmez).
- `.razor` degistirdikten sonra: `npm run build:css`
- Gelistirme sirasinda: `npm run watch:css`

## WebUI'den Farklar
- Sidebar/mobil menu ve tab gecisleri (Son Islemler/Bekleyen/Basarisiz) vanilla JS yerine Blazor'un C# durum yonetimi (interactivity) ile calisir, sayfa yenilenmeden guncellenir.
- Aktif menu vurgusu `NavLink` bileseni ile otomatik yapilir.
