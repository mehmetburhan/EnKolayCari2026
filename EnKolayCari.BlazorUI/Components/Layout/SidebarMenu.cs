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

/// <summary>Sidebar menu tree. Every leaf item routes to a mockup index page.</summary>
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
                new MenuItem { Label = "Müşteri & Tedarikçi", IconSvg = MenuIcons.Users, Href = "customers-suppliers" },
                new MenuItem { Label = "Ürün", IconSvg = MenuIcons.Cube, Href = "products" },
                new MenuItem { Label = "Hizmet", IconSvg = MenuIcons.Wrench, Href = "services" },
                new MenuItem { Label = "Depo", IconSvg = MenuIcons.ArchiveBox, Href = "warehouses" },
                new MenuItem { Label = "Stok & Depo Hareketleri", IconSvg = MenuIcons.ArrowsRightLeft, Href = "stock-movements" },
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
                        new MenuItem { Label = "Teklifler", IconSvg = MenuIcons.DocumentText, Href = "sales/quotes" },
                        new MenuItem { Label = "Siparişler", IconSvg = MenuIcons.ClipboardList, Href = "sales/orders" },
                        new MenuItem { Label = "Satış Faturaları", IconSvg = MenuIcons.DocumentText, Href = "sales/invoices" },
                    ],
                },
                new MenuItem
                {
                    Label = "Alışlar",
                    IconSvg = MenuIcons.ShoppingBag,
                    Children =
                    [
                        new MenuItem { Label = "Alış Faturaları", IconSvg = MenuIcons.DocumentText, Href = "purchases/invoices" },
                        new MenuItem { Label = "Alış Siparişleri", IconSvg = MenuIcons.Truck, Href = "purchases/orders" },
                    ],
                },
                new MenuItem { Label = "Giderler", IconSvg = MenuIcons.CreditCard, Href = "expenses" },
                new MenuItem
                {
                    Label = "E-Faturalar",
                    IconSvg = MenuIcons.Inbox,
                    Children =
                    [
                        new MenuItem { Label = "Gelen e-Faturalar", IconSvg = MenuIcons.ArrowDownTray, Href = "e-invoices/incoming" },
                        new MenuItem { Label = "Giden e-Faturalar", IconSvg = MenuIcons.ArrowUpTray, Href = "e-invoices/outgoing" },
                    ],
                },
            ],
        },
        new MenuGroup
        {
            Label = "ARAÇLAR",
            Items =
            [
                new MenuItem { Label = "Kasa", IconSvg = MenuIcons.Banknotes, Href = "cashbox" },
                new MenuItem { Label = "Banka Hesapları", IconSvg = MenuIcons.BuildingLibrary, Href = "bank-accounts" },
                new MenuItem { Label = "Çekler", IconSvg = MenuIcons.DocumentText, Href = "checks" },
                new MenuItem { Label = "Nakit Durumu", IconSvg = MenuIcons.ChartBar, Href = "cash-flow" },
                new MenuItem { Label = "Banka Mutabakatı", IconSvg = MenuIcons.Scale, Href = "bank-reconciliation" },
            ],
        },
        new MenuGroup
        {
            Label = "ENTEGRASYONLAR",
            Items =
            [
                new MenuItem { Label = "E-Ticaret", IconSvg = MenuIcons.Globe, Href = "integrations/e-commerce" },
                new MenuItem { Label = "Banka", IconSvg = MenuIcons.BuildingLibrary, Href = "integrations/bank" },
            ],
        },
        new MenuGroup
        {
            Label = "TANIMLAR & AYARLAR",
            Items =
            [
                new MenuItem { Label = "Kullanıcılar", IconSvg = MenuIcons.UserCircle, Href = "users" },
                new MenuItem { Label = "Firma Ayarları", IconSvg = MenuIcons.Cog, Href = "company-settings" },
                new MenuItem { Label = "Etiket Tasarımı", IconSvg = MenuIcons.Tag, Href = "label-design" },
                new MenuItem
                {
                    Label = "Belge Tasarımları",
                    IconSvg = MenuIcons.DocumentDuplicate,
                    Children =
                    [
                        new MenuItem { Label = "Fatura", IconSvg = MenuIcons.DocumentText, Href = "document-designs/invoice" },
                        new MenuItem { Label = "İrsaliye", IconSvg = MenuIcons.Truck, Href = "document-designs/waybill" },
                        new MenuItem { Label = "Satış Formu", IconSvg = MenuIcons.DocumentText, Href = "document-designs/sales-form" },
                        new MenuItem { Label = "Teklif Formu", IconSvg = MenuIcons.DocumentText, Href = "document-designs/quote-form" },
                        new MenuItem { Label = "Kargo Etiket", IconSvg = MenuIcons.Tag, Href = "document-designs/shipping-label" },
                    ],
                },
            ],
        },
    ];
}
