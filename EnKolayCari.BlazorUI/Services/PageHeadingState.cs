namespace EnKolayCari.BlazorUI.Services;

public class PageHeadingState
{
    public string Title { get; private set; } = "Panel";
    public string? Subtitle { get; private set; }

    public event Action? Changed;

    public void Set(string title, string? subtitle = null)
    {
        Title = title;
        Subtitle = subtitle;
        Changed?.Invoke();
    }
}
