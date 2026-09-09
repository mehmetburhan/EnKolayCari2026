namespace EnKolayCari2026.DataBridge.Configuration;

public sealed class SqlEndpoint
{
    public string Server { get; set; } = "";
    public string Database { get; set; } = "";
    public string User { get; set; } = "";
    public string Password { get; set; } = "";

    public string BuildConnectionString() =>
        $"Server={Server};Database={Database};User Id={User};Password={Password};TrustServerCertificate=True;Encrypt=True;Connect Timeout=30;";
}

public sealed class AppConnections
{
    public SqlEndpoint Master { get; set; } = new();
    public SqlEndpoint Slave { get; set; } = new();
    public SqlEndpoint Target { get; set; } = new();
}

public sealed class TransferOptions
{
    public int BatchSize { get; set; } = 100;
    public bool SkipExistingByGId { get; set; } = true;
}
