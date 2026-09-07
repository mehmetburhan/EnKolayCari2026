# EnKolayCari.BlazorUI

## Amaç
Projenin sunum katmanı. Blazor Web App, Interactive Server render modu ile çalışır. HTTP sadece Web.Common üzerinden.

## Bağımlılıklar
Web.Common, Language.

## DO
- Katman amacına uygun kod yaz
- Namespace: `EnKolayCari.BlazorUI.*`
- Sayfa başlığı/alt başlığı için `<PageHeading Title="..." Subtitle="..." />` bileşeni kullan (Components/Layout/PageHeading.razor)

## DON'T
- Domain/Persistence referansı

## Styling (Tailwind CSS)
- Nexora tema: sidebar + header dashboard layout, ayrı bir auth (giriş/şifre) shell.
- Kaynak: `Styles/app.css`. Üretilen dosya: `wwwroot/app.css` (elle düzenlenmez).
- `.razor` değiştirdikten sonra: `npm run build:css`
- Geliştirme sırasında: `npm run watch:css`

## Etkileşim
- Sidebar/mobil menü, açılır alt menüler ve tab geçişleri (Son İşlemler/Bekleyen/Başarısız) vanilla JS yerine Blazor'un C# durum yönetimi (interactivity) ile çalışır, sayfa yenilenmeden güncellenir.
- Aktif menü vurgusu `NavLink` bileşeni ile otomatik yapılır.

## Kimlik Doğrulama (UI)
- `/auth`, `/sifremi-unuttum`, `/sifre-sifirla` sayfaları `AuthLayout` (sidebar'sız) kullanır, `Components/Auth/AuthShell.razor` ortak iki kolonlu kabuğu sağlar.
- Şu an yalnızca arayüz: gerçek kimlik doğrulama/backend bağlantısı yok, route guard yok.
