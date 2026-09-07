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
- Sayfa iskeleti (layout, sidebar, header, card, kpi) mevcut Tailwind utility class'ları ve `@layer components` sınıflarıyla kurulur; bu desen korunur.

## Component Kütüphanesi (LumexUI)
- Yeni sayfalarda input, form, buton, data grid gibi bileşenler için [LumexUI](https://lumexui.org) kullanılır (`LumexButton`, `LumexInput`, `LumexCard`, `LumexDataGrid` vb.). LumexUI, MudBlazor'un aksine kendi başına bir CSS framework'ü getirmez; bileşenleri doğrudan Tailwind utility class'larıyla oluşturur ve **derleme sırasında Tailwind'in kendisine eklenir**, bu yüzden mevcut Nexora/Tailwind tasarımıyla çakışmaz.
- Kurulum:
  - `Program.cs`: `builder.Services.AddLumexServices();`
  - `Components/_Imports.razor`: `@using LumexUI`
  - `Components/App.razor`: `<script src="_content/LumexUI/js/LumexUI.js" type="module"></script>`
  - `Styles/app.css`: `@import "../bin/lumexui/theme.css";` (Tailwind'in `@import "tailwindcss";` satırından hemen sonra) ve `@source "../bin/lumexui/*.cs";` — LumexUI paketinin derleme sırasında `bin/lumexui/` altına çıkardığı tema dosyasını ve bileşenlerin kullandığı sınıfları Tailwind'e tanıtır. Bu klasör `bin/` altında olduğu için build'e bağımlıdır, elle düzenlenmez.
- Tema: LumexUI'nin `--lumex-primary/secondary/focus/success/warning` token'ları `Styles/app.css` içindeki `:root` bloğunda Nexora paletiyle (`--nexora-blue/violet/teal/orange`) eşleştirildi, böylece LumexUI bileşenleri de marka renkleriyle render olur. `--default-transition-duration` LumexUI'nin değiştirdiği değerden (250ms) Tailwind'in orijinal 150ms değerine geri alındı ki mevcut `transition-*` utility'leri (ör. sidebar chevron) etkilenmesin.
- `.razor`/bileşen değişikliklerinden sonra `npm run build:css` her zaman gerekli — LumexUI de dahil tüm sınıflar bu adımda üretiliyor.
- Yeni bir LumexUI bileşeni entegre edildiğinde, `npm run build:css` sonrası `wwwroot/app.css`'te var olan (LumexUI öncesi) sınıfların değerlerinin **değişmediğini** doğrulamadan commit atma — MudBlazor denemesinde tüm tasarımı bozan `!important` çakışması bu kontrolün eksikliğinden kaynaklanmıştı.

## Etkileşim
- Sidebar/mobil menü, açılır alt menüler ve tab geçişleri (Son İşlemler/Bekleyen/Başarısız) vanilla JS yerine Blazor'un C# durum yönetimi (interactivity) ile çalışır, sayfa yenilenmeden güncellenir.
- Aktif menü vurgusu `NavLink` bileşeni ile otomatik yapılır.
- Kullanıcı menüsü (`Components/Layout/UserMenu.razor`, TopHeader'da) profil, firma değiştirme, dil değiştirme ve karanlık mod içerir; hepsi `Services/SessionState.cs` (bir `Changed` event'i olan scoped servis, `PageHeadingState` ile aynı desen) üzerinden yönetilir.
- **Karanlık mod**: JS/localStorage yok. `MainLayout.razor` kök `<div class="app-shell ...">`'ine `Session.IsDarkMode` durumuna göre `dark` class'ı ekleniyor; `AuthLayout` bu wrapper'ı kullanmadığı için giriş sayfaları her zaman açık temada kalıyor. Karşılık gelen `.dark ...` override'ları `Styles/app.css`'in sonunda **layer'sız (unlayered)** olarak tanımlı — Tailwind'in `@layer utilities`'i `@layer components`'ten önceliklidir, bu yüzden layer içine yazılan bir override Tailwind'in kendi utility'sini geçemez; unlayered kural her zaman kazanır. Yeni bir sayfa/renk eklerken o class'ı burada da (aynı desenle: `.dark .text-slate-XXX { color: ... }`) karşılıksız bırakma, yoksa o metin karanlık modda okunaksız kalır.

## Kimlik Doğrulama (UI)
- `/auth`, `/sifremi-unuttum`, `/sifre-sifirla` sayfaları `AuthLayout` (sidebar'sız) kullanır, `Components/Auth/AuthShell.razor` ortak iki kolonlu kabuğu sağlar.
- Şu an yalnızca arayüz: gerçek kimlik doğrulama/backend bağlantısı yok, route guard yok.
