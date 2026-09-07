namespace EnKolayCari.BlazorUI.Models;

public class DashboardData
{
    public string UserFirstName { get; set; } = string.Empty;

    public required Kpi TotalRevenue { get; set; }
    public required Kpi NewCustomers { get; set; }
    public required Kpi ConversionRate { get; set; }
    public required Kpi OrdersCompleted { get; set; }

    public string TotalBalanceLabel { get; set; } = string.Empty;
    public List<CurrencyAccount> CurrencyAccounts { get; set; } = [];

    public List<Transaction> RecentTransactions { get; set; } = [];
    public List<Transaction> PendingTransactions { get; set; } = [];
    public List<Transaction> FailedTransactions { get; set; } = [];

    public List<Goal> Goals { get; set; } = [];
}

public class Kpi
{
    public required string ValueLabel { get; set; }
    public required string ChangeLabel { get; set; }
    public bool IsPositive { get; set; } = true;
}

public class CurrencyAccount
{
    public required string CountryCode { get; set; }
    public required string CurrencyCode { get; set; }
    public required string AmountLabel { get; set; }
}

public class Transaction
{
    public required string Description { get; set; }
    public required string Type { get; set; }
    public required string DateLabel { get; set; }
    public required string AmountLabel { get; set; }
    public bool IsPositive { get; set; }
    public required string Status { get; set; }
    public bool IsStatusSuccess { get; set; }
}

public class Goal
{
    public required string Title { get; set; }
    public required string CurrentLabel { get; set; }
    public required string TargetLabel { get; set; }
    public int PercentComplete { get; set; }
    public required string AccentClass { get; set; }
}
