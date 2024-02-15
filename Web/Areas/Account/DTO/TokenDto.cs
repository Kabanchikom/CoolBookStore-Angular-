using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Account.Contracts;

public class TokenDto
{
    [Display(Name = "Access token")]
    public string? AccessToken { get; set; }

    [Display(Name = "Refresh token")]
    public string? RefreshToken { get; set; }
}