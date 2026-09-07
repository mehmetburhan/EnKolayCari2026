using EnKolayCari.BlazorUI.Models;

namespace EnKolayCari.BlazorUI.Services;

public enum AppLanguage
{
    Turkish,
    English,
    German,
    Russian,
}

public class SessionState
{
    public string UserName { get; private set; } = "Celal Dağdeviren";
    public string UserEmail { get; private set; } = "celal@enkolaycari.com";
    public string CompanyName { get; private set; } = DemoCompanies.All[0].Name;
    public AppLanguage Language { get; private set; } = AppLanguage.Turkish;

    public event Action? Changed;

    public void SetLanguage(AppLanguage language)
    {
        if (Language == language)
        {
            return;
        }

        Language = language;
        Changed?.Invoke();
    }
}
