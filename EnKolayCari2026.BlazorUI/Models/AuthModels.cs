using System.ComponentModel.DataAnnotations;

namespace EnKolayCari2026.BlazorUI.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Firma seçimi zorunludur.")]
    public string CompanyId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}

public class ForgotPasswordModel
{
    [Required(ErrorMessage = "Firma seçimi zorunludur.")]
    public string CompanyId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kullanıcı adı veya e-posta zorunludur.")]
    public string UsernameOrEmail { get; set; } = string.Empty;
}

public class ResetPasswordModel
{
    [Required(ErrorMessage = "Yeni şifre zorunludur.")]
    [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
    [Compare(nameof(NewPassword), ErrorMessage = "Şifreler eşleşmiyor.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public static class DemoCompanies
{
    public static readonly (string Id, string Name)[] All =
    [
        ("1", "EnKolayCari Demo A.Ş."),
        ("2", "Acme Ticaret Ltd. Şti."),
        ("3", "Best Firma A.Ş."),
    ];
}
