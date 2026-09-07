using EnKolayCari2026.BlazorUI.Models;

namespace EnKolayCari2026.BlazorUI.Services;

public static class DashboardDemoData
{
    public static DashboardData GetSample()
    {
        return new DashboardData
        {
            UserFirstName = "Celal",
            TotalRevenue = new Kpi { ValueLabel = "₺58.420,50", ChangeLabel = "%18,2", IsPositive = true },
            NewCustomers = new Kpi { ValueLabel = "268", ChangeLabel = "%12,5", IsPositive = true },
            ConversionRate = new Kpi { ValueLabel = "%7,68", ChangeLabel = "%8,1", IsPositive = true },
            OrdersCompleted = new Kpi { ValueLabel = "1.426", ChangeLabel = "%15,7", IsPositive = true },

            TotalBalanceLabel = "₺24.560,75",
            CurrencyAccounts =
            [
                new CurrencyAccount { CountryCode = "TR", CurrencyCode = "TRY", AmountLabel = "₺10.420,30" },
                new CurrencyAccount { CountryCode = "EU", CurrencyCode = "EUR", AmountLabel = "€7.850,20" },
                new CurrencyAccount { CountryCode = "US", CurrencyCode = "USD", AmountLabel = "$4.620,15" },
                new CurrencyAccount { CountryCode = "GB", CurrencyCode = "GBP", AmountLabel = "£1.670,10" },
            ],

            RecentTransactions =
            [
                new Transaction { Description = "ABC Ticaret Ltd.", Type = "Tahsilat", DateLabel = "12 Eki 2026", AmountLabel = "+₺2.450,00", IsPositive = true, Status = "Basarili", IsStatusSuccess = true },
                new Transaction { Description = "Ofis Kirasi", Type = "Odeme", DateLabel = "11 Eki 2026", AmountLabel = "-₺890,00", IsPositive = false, Status = "Tamamlandi", IsStatusSuccess = false },
                new Transaction { Description = "Reklam Gideri", Type = "Odeme", DateLabel = "10 Eki 2026", AmountLabel = "-₺3.205,00", IsPositive = false, Status = "Tamamlandi", IsStatusSuccess = false },
                new Transaction { Description = "XYZ Sanayi A.S.", Type = "Tahsilat", DateLabel = "9 Eki 2026", AmountLabel = "+₺11.500,00", IsPositive = true, Status = "Basarili", IsStatusSuccess = true },
                new Transaction { Description = "Kirtasiye Gideri", Type = "Odeme", DateLabel = "9 Eki 2026", AmountLabel = "-₺459,90", IsPositive = false, Status = "Tamamlandi", IsStatusSuccess = false },
            ],

            PendingTransactions =
            [
                new Transaction { Description = "Depo Kirasi", Type = "Odeme", DateLabel = "13 Eki 2026", AmountLabel = "-₺1.200,00", IsPositive = false, Status = "Beklemede", IsStatusSuccess = false },
                new Transaction { Description = "DEF Insaat Ltd.", Type = "Tahsilat", DateLabel = "13 Eki 2026", AmountLabel = "+₺6.750,00", IsPositive = true, Status = "Beklemede", IsStatusSuccess = false },
            ],

            FailedTransactions =
            [
                new Transaction { Description = "Yazilim Aboneligi", Type = "Odeme", DateLabel = "8 Eki 2026", AmountLabel = "-₺349,00", IsPositive = false, Status = "Basarisiz", IsStatusSuccess = false },
            ],

            Goals =
            [
                new Goal { Title = "Aylik Ciro Hedefi", CurrentLabel = "₺75.000", TargetLabel = "₺100.000", PercentComplete = 75, AccentClass = "bg-[var(--nexora-blue)]" },
                new Goal { Title = "Yeni Musteri", CurrentLabel = "268", TargetLabel = "500", PercentComplete = 54, AccentClass = "bg-[var(--nexora-teal)]" },
                new Goal { Title = "Tamamlanan Siparis", CurrentLabel = "1.426", TargetLabel = "2.000", PercentComplete = 71, AccentClass = "bg-[var(--nexora-orange)]" },
            ],
        };
    }
}
