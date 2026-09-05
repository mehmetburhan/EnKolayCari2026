using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EnKolayCari2026.WebUI.Models;

namespace EnKolayCari2026.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var model = BuildDashboardDemoData();

        ViewData["PageTitle"] = $"Hos geldiniz, {model.UserFirstName}!";
        ViewData["PageSubtitle"] = "Isletmenizin genel performansina goz atin.";

        return View(model);
    }

    public IActionResult Privacy()
    {
        ViewData["PageTitle"] = "Gizlilik Politikasi";

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        ViewData["PageTitle"] = "Hata";

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private static DashboardViewModel BuildDashboardDemoData()
    {
        return new DashboardViewModel
        {
            UserFirstName = "Celal",
            TotalRevenue = new KpiViewModel { ValueLabel = "₺58.420,50", ChangeLabel = "%18,2", IsPositive = true },
            NewCustomers = new KpiViewModel { ValueLabel = "268", ChangeLabel = "%12,5", IsPositive = true },
            ConversionRate = new KpiViewModel { ValueLabel = "%7,68", ChangeLabel = "%8,1", IsPositive = true },
            OrdersCompleted = new KpiViewModel { ValueLabel = "1.426", ChangeLabel = "%15,7", IsPositive = true },

            TotalBalanceLabel = "₺24.560,75",
            CurrencyAccounts =
            [
                new CurrencyAccountViewModel { CountryCode = "TR", CurrencyCode = "TRY", AmountLabel = "₺10.420,30" },
                new CurrencyAccountViewModel { CountryCode = "EU", CurrencyCode = "EUR", AmountLabel = "€7.850,20" },
                new CurrencyAccountViewModel { CountryCode = "US", CurrencyCode = "USD", AmountLabel = "$4.620,15" },
                new CurrencyAccountViewModel { CountryCode = "GB", CurrencyCode = "GBP", AmountLabel = "£1.670,10" },
            ],

            RecentTransactions =
            [
                new TransactionViewModel { Description = "ABC Ticaret Ltd.", Type = "Tahsilat", DateLabel = "12 Eki 2026", AmountLabel = "+₺2.450,00", IsPositive = true, Status = "Basarili", IsStatusSuccess = true },
                new TransactionViewModel { Description = "Ofis Kirasi", Type = "Odeme", DateLabel = "11 Eki 2026", AmountLabel = "-₺890,00", IsPositive = false, Status = "Tamamlandi", IsStatusSuccess = false },
                new TransactionViewModel { Description = "Reklam Gideri", Type = "Odeme", DateLabel = "10 Eki 2026", AmountLabel = "-₺3.205,00", IsPositive = false, Status = "Tamamlandi", IsStatusSuccess = false },
                new TransactionViewModel { Description = "XYZ Sanayi A.S.", Type = "Tahsilat", DateLabel = "9 Eki 2026", AmountLabel = "+₺11.500,00", IsPositive = true, Status = "Basarili", IsStatusSuccess = true },
                new TransactionViewModel { Description = "Kirtasiye Gideri", Type = "Odeme", DateLabel = "9 Eki 2026", AmountLabel = "-₺459,90", IsPositive = false, Status = "Tamamlandi", IsStatusSuccess = false },
            ],

            Goals =
            [
                new GoalViewModel { Title = "Aylik Ciro Hedefi", CurrentLabel = "₺75.000", TargetLabel = "₺100.000", PercentComplete = 75, AccentClass = "bg-[var(--nexora-blue)]" },
                new GoalViewModel { Title = "Yeni Musteri", CurrentLabel = "268", TargetLabel = "500", PercentComplete = 54, AccentClass = "bg-[var(--nexora-teal)]" },
                new GoalViewModel { Title = "Tamamlanan Siparis", CurrentLabel = "1.426", TargetLabel = "2.000", PercentComplete = 71, AccentClass = "bg-[var(--nexora-orange)]" },
            ],
        };
    }
}
