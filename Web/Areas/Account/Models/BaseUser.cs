using Microsoft.AspNetCore.Identity;
using Web.Models;

namespace Web.Areas.Account.Models;

public abstract class BaseUser : IdentityUser
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; }
    
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpAt { get; set; }
}