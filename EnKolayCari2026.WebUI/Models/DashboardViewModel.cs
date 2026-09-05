namespace EnKolayCari2026.WebUI.Models;

public class DashboardViewModel
{
    public string UserFirstName { get; set; } = string.Empty;

    public required KpiViewModel TotalRevenue { get; set; }
    public required KpiViewModel NewCustomers { get; set; }
    public required KpiViewModel ConversionRate { get; set; }
    public required KpiViewModel OrdersCompleted { get; set; }

    public string TotalBalanceLabel { get; set; } = string.Empty;
    public List<CurrencyAccountViewModel> CurrencyAccounts { get; set; } = [];

    public List<TransactionViewModel> RecentTransactions { get; set; } = [];
    public List<GoalViewModel> Goals { get; set; } = [];
}

public class KpiViewModel
{
    public required string ValueLabel { get; set; }
    public required string ChangeLabel { get; set; }
    public bool IsPositive { get; set; } = true;
}

public class CurrencyAccountViewModel
{
    public required string CountryCode { get; set; }
    public required string CurrencyCode { get; set; }
    public required string AmountLabel { get; set; }
}

public class TransactionViewModel
{
    public required string Description { get; set; }
    public required string Type { get; set; }
    public required string DateLabel { get; set; }
    public required string AmountLabel { get; set; }
    public bool IsPositive { get; set; }
    public required string Status { get; set; }
    public bool IsStatusSuccess { get; set; }
}

public class GoalViewModel
{
    public required string Title { get; set; }
    public required string CurrentLabel { get; set; }
    public required string TargetLabel { get; set; }
    public int PercentComplete { get; set; }
    public required string AccentClass { get; set; }
}
