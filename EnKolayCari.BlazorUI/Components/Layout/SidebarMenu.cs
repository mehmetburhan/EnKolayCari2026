namespace EnKolayCari.BlazorUI.Components.Layout;

public class MenuItem
{
    public required string Label { get; set; }
    public required string IconSvg { get; set; }
    public string Href { get; set; } = "#";
    public bool ExactMatch { get; set; }
    public List<MenuItem>? Children { get; set; }
}

public class MenuGroup
{
    public required string Label { get; set; }
    public required List<MenuItem> Items { get; set; }
}

/// <summary>Sidebar menu tree. Only "Genel Bakış" (Panel) has a real route today; the rest are placeholders reserved for future pages.</summary>
public static class SidebarMenu
{
    public static readonly List<MenuGroup> Groups =
    [
        new MenuGroup
        {
            Label = "GENEL",
            Items =
            [
                new MenuItem { Label = "Genel Bakış (Panel)", IconSvg = MenuIcons.Home, Href = "", ExactMatch = true },
                new MenuItem { Label = "Müşteri & Tedarikçi", IconSvg = MenuIcons.Users },
                new MenuItem { Label = "Ürün", IconSvg = MenuIcons.Cube },
                new MenuItem { Label = "Hizmet", IconSvg = MenuIcons.Wrench },
                new MenuItem { Label = "Depo", IconSvg = MenuIcons.ArchiveBox },
                new MenuItem { Label = "Stok & Depo Hareketleri", IconSvg = MenuIcons.ArrowsRightLeft },
            ],
        },
        new MenuGroup
        {
            Label = "FINANCE",
            Items =
            [
                new MenuItem
                {
                    Label = "Satışlar",
                    IconSvg = MenuIcons.ShoppingCart,
                    Children =
                    [
                        new MenuItem { Label = "Teklifler", IconSvg = MenuIcons.DocumentText },
                        new MenuItem { Label = "Siparişler", IconSvg = MenuIcons.ClipboardList },
                        new MenuItem { Label = "Satış Faturaları", IconSvg = MenuIcons.DocumentText },
                    ],
                },
                new MenuItem
                {
                    Label = "Alışlar",
                    IconSvg = MenuIcons.ShoppingBag,
                    Children =
                    [
                        new MenuItem { Label = "Alış Faturaları", IconSvg = MenuIcons.DocumentText },
                        new MenuItem { Label = "Alış Siparişleri", IconSvg = MenuIcons.Truck },
                    ],
                },
                new MenuItem { Label = "Giderler", IconSvg = MenuIcons.CreditCard },
                new MenuItem
                {
                    Label = "E-Faturalar",
                    IconSvg = MenuIcons.Inbox,
                    Children =
                    [
                        new MenuItem { Label = "Gelen e-Faturalar", IconSvg = MenuIcons.ArrowDownTray },
                        new MenuItem { Label = "Giden e-Faturalar", IconSvg = MenuIcons.ArrowUpTray },
                    ],
                },
            ],
        },
        new MenuGroup
        {
            Label = "ARAÇLAR",
            Items =
            [
                new MenuItem { Label = "Kasa", IconSvg = MenuIcons.Banknotes },
                new MenuItem { Label = "Banka Hesapları", IconSvg = MenuIcons.BuildingLibrary },
                new MenuItem { Label = "Çekler", IconSvg = MenuIcons.DocumentText },
                new MenuItem { Label = "Nakit Durumu", IconSvg = MenuIcons.ChartBar },
                new MenuItem { Label = "Banka Mutabakatı", IconSvg = MenuIcons.Scale },
            ],
        },
        new MenuGroup
        {
            Label = "ENTEGRASYONLAR",
            Items =
            [
                new MenuItem { Label = "E-Ticaret", IconSvg = MenuIcons.Globe },
                new MenuItem { Label = "Banka", IconSvg = MenuIcons.BuildingLibrary },
            ],
        },
        new MenuGroup
        {
            Label = "TANIMLAR & AYARLAR",
            Items =
            [
                new MenuItem { Label = "Kullanıcılar", IconSvg = MenuIcons.UserCircle },
                new MenuItem { Label = "Firma Ayarları", IconSvg = MenuIcons.Cog },
                new MenuItem { Label = "Etiket Tasarımı", IconSvg = MenuIcons.Tag },
                new MenuItem
                {
                    Label = "Belge Tasarımları",
                    IconSvg = MenuIcons.DocumentDuplicate,
                    Children =
                    [
                        new MenuItem { Label = "Fatura", IconSvg = MenuIcons.DocumentText },
                        new MenuItem { Label = "İrsaliye", IconSvg = MenuIcons.Truck },
                        new MenuItem { Label = "Satış Formu", IconSvg = MenuIcons.DocumentText },
                        new MenuItem { Label = "Teklif Formu", IconSvg = MenuIcons.DocumentText },
                        new MenuItem { Label = "Kargo Etiket", IconSvg = MenuIcons.Tag },
                    ],
                },
            ],
        },
    ];
}
