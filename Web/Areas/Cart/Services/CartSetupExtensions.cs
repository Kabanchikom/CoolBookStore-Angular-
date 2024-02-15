namespace Web.Areas.Cart.Services;

public static class ProductSetupExtensions
{
    public static IServiceCollection AddCart(this IServiceCollection services)
    {
        services.AddScoped<CartService>();

        return services;
    }
}