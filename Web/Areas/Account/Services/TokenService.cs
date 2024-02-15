using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Web.Areas.Account.Contracts;
using Web.Areas.Account.Models;
using Web.Areas.Account.Options;

namespace Web.Areas.Account.Services;

public class TokenService
{
    private readonly AuthOptions _options;
    
    private SymmetricSecurityKey AccessKey => new (Encoding.UTF8.GetBytes(_options.AccessToken.SecretKey));
    private SymmetricSecurityKey RefreshKey => new (Encoding.UTF8.GetBytes(_options.RefreshToken.SecretKey));

    public TokenService(IOptions<AuthOptions> options)
    {
        _options = options.Value;
    }

    public JwtSecurityToken CreateAccessToken(List<Claim> authClaims)
    {
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            expires: DateTime.Now.AddMinutes(_options.AccessToken.ExpTimeInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(AccessKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
    
    public JwtSecurityToken CreateRefreshToken(List<Claim> authClaims)
    {
        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            expires: DateTime.Now.AddMinutes(_options.RefreshToken.ExpTimeInDays),
            claims: authClaims,
            signingCredentials: new SigningCredentials(RefreshKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    public ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = _options.Audience,
            ValidateIssuer = true,
            ValidIssuer = _options.Issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = RefreshKey,
            ValidateLifetime = false,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        
        SecurityToken securityToken;
        ClaimsPrincipal? principal = null;
        
        try
        {
            principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        }
        catch (SecurityTokenSignatureKeyNotFoundException e)
        {
            tokenValidationParameters.IssuerSigningKey = AccessKey;
            principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        }
        
        if (securityToken
                is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken
                .Header
                .Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        
        return principal;
    }
}