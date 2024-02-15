using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Web.Areas.Account.Contracts;
using Web.Areas.Account.DTO;
using Web.Areas.Account.Models;
using Web.Areas.Account.Options;

namespace Web.Areas.Account.Services;

public class AccountService
{
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;
    private readonly AuthOptions _options;
    private readonly IMapper _mapper;

    public AccountService(
        UserManager<User> userManager,
        IOptions<AuthOptions> options,
        TokenService tokenService,
        IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _options = options.Value;
        _mapper = mapper;
    }

    public async Task<UserDto> GetUserAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<(SignInResult, TokenDto?)> LoginAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null || !await _userManager.CheckPasswordAsync(user, password))
        {
            return (SignInResult.Failed, null);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
        };

        var accessToken = _tokenService.CreateAccessToken(claims);
        var refreshToken = _tokenService.CreateRefreshToken(claims);
        
        user.RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);
        user.RefreshTokenExpAt = refreshToken.ValidTo;

        await _userManager.UpdateAsync(user);

        return (SignInResult.Success, new TokenDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken),
        });
    }
    
    public async Task<(IdentityResult, TokenDto?)> RegisterAsync(User user, string password)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
        };
        
        var refreshToken = _tokenService.CreateRefreshToken(claims);
        user.RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);
        user.RefreshTokenExpAt = refreshToken.ValidTo;
        
        var identityResult = await _userManager.CreateAsync(user, password);

        if (identityResult != IdentityResult.Success)
        {
            return (identityResult, null);
        }

        var accessToken = _tokenService.CreateAccessToken(claims);
        
        
        return (identityResult, new TokenDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken),
        });
    }

    public async Task<TokenDto?> RefreshTokensAsync(string? refreshToken)
    {
        if (refreshToken is null)
        {
            throw new ArgumentException("Invalid refresh token");
        }

        var principal = _tokenService.GetPrincipalFromToken(refreshToken);

        if (principal == null)
        {
            throw new ArgumentException("Invalid access token or refresh token");
        }
        
        var username = principal.Identity?.Name;

        var user = await _userManager.FindByNameAsync(username);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpAt <= DateTime.UtcNow)
        {
            throw new ArgumentException("Invalid access token or refresh token");
        }

        var newAccessToken = _tokenService.CreateAccessToken(principal.Claims.ToList());
        var newRefreshToken = _tokenService.CreateRefreshToken(principal.Claims.ToList());

        var tokenHandler = new JwtSecurityTokenHandler();

        user.RefreshToken = tokenHandler.WriteToken(newRefreshToken);
        user.RefreshTokenExpAt = newRefreshToken.ValidTo;
        await _userManager.UpdateAsync(user);

        return new TokenDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken =  tokenHandler.WriteToken(newRefreshToken),
        };
    }

    public async Task LogoutAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        user.RefreshToken = null;
        user.RefreshTokenExpAt = null;
        await _userManager.UpdateAsync(user);
    }

    public static Guid ExtractUserId(HttpContext context)
    {
        return Guid.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}