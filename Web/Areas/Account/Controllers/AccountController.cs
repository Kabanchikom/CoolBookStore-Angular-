using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Account.Contracts;
using Web.Areas.Account.DTO;
using Web.Areas.Account.Models;
using Web.Areas.Account.Services;
using Web.Filters;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Web.Areas.Account.Controllers;

[Route("api/account")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly AccountService _accountService;
    private readonly TokenService _tokenService;

    public AccountController(
        AccountService accountService, 
        IMapper mapper, 
        TokenService tokenService)
    {
        _accountService = accountService;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    [HttpPost("login"), ValidateModel, AllowAnonymous]
    public async Task<ActionResult<TokenDto?>> Login([FromBody] LoginRequestDto requestDto)
    {
        var (signInResult, tokenModel) = await _accountService.LoginAsync(requestDto.UserName, requestDto.Password);

        if (signInResult == SignInResult.Failed)
        {
            return Unauthorized("Неверный логин или пароль");
        }

        // temp
        HttpContext.Response.Cookies.Append("accessToken", tokenModel!.AccessToken!,
            new CookieOptions
            {
                HttpOnly = true,
            });

        HttpContext.Response.Cookies.Append("refreshToken", tokenModel!.RefreshToken!,
            new CookieOptions
            {
                HttpOnly = true,
            });

        return tokenModel;
    }
    
    [HttpPost("register"), ValidateModel, AllowAnonymous]
    public async Task<ActionResult<TokenDto?>> Register([FromBody] RegisterRequestDto requestDto)
    {
        var user = _mapper.Map<User>(requestDto);
        var (identityResult, tokenModel) = await _accountService.RegisterAsync(user, requestDto.Password);

        if (identityResult != IdentityResult.Success)
        {
            return Unauthorized("Неверно указаны данные при регистрации");
        }

        // temp
        HttpContext.Response.Cookies.Append("accessToken", tokenModel!.AccessToken!,
            new CookieOptions
            {
                HttpOnly = true,
            });

        HttpContext.Response.Cookies.Append("refreshToken", tokenModel!.RefreshToken!,
            new CookieOptions
            {
                HttpOnly = true,
            });

        return tokenModel;
    }
    
    [HttpGet("refresh"), AllowAnonymous]
    public async Task<ActionResult<TokenDto?>> Refresh()
    {
        var refreshToken = HttpContext.Request.Cookies["refreshToken"];
        TokenDto? tokenModel = null;
        
        try
        {
            tokenModel = await _accountService.RefreshTokensAsync(refreshToken);
        }
        catch (ArgumentException e)
        {
            return Unauthorized("Refresh-токен или Access-токен невалидны");
        }

        // temp
        HttpContext.Response.Cookies.Append("accessToken", tokenModel!.AccessToken!,
            new CookieOptions
            {
                HttpOnly = true,
            });

        HttpContext.Response.Cookies.Append("refreshToken", tokenModel!.RefreshToken!,
            new CookieOptions
            {
                HttpOnly = true,
            });
        
        return tokenModel;
    }

    [HttpPatch("logout")]
    public async Task<ActionResult> Logout()
    {
        var refreshToken = HttpContext.Request.Cookies["refreshToken"];

        if (refreshToken == null)
        {
            return Unauthorized();
        }
        
        var username = _tokenService.GetPrincipalFromToken(refreshToken)?.Identity?.Name;
        
        if (username == null)
        {
            return Unauthorized();
        }

        HttpContext.Response.Cookies.Delete("refreshToken");
        HttpContext.Response.Cookies.Delete("accessToken");

        await _accountService.LogoutAsync(username);
        return Ok();
    }

    [HttpGet("user")]
    public async Task<ActionResult<UserDto>> GetUser()
    {
        var refreshToken = HttpContext.Request.Cookies["refreshToken"];

        if (refreshToken == null)
        {
            return Unauthorized();
        }

        var username = _tokenService.GetPrincipalFromToken(refreshToken)?.Identity?.Name;

        if (username == null)
        {
            return Unauthorized();
        }

        var user = await _accountService.GetUserAsync(username);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }
}