using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Web.Areas.Account.Models;
using Web.Areas.Account.Options;
using Web.EF;

namespace Web.Areas.Account.Services;

public static class AccountSetupExtensions
{
    public static IServiceCollection AddAccount(this IServiceCollection services, IConfigurationSection configuration)
    {
        services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<BookStoreDbContext>();
        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Issuer"],
                    ValidAudience = configuration["Audience"],
                    ValidateAudience = true,
                    ValidateLifetime = bool.Parse(configuration["ValidateLifetime"] ?? "false"),
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["AccessToken:SecretKey"])),
                    ValidateIssuerSigningKey = true,
                };
            });
        
        services.Configure<AuthOptions>(configuration);
        services.AddScoped<TokenService>();
        services.AddScoped<AccountService>();

        return services;
    }
}