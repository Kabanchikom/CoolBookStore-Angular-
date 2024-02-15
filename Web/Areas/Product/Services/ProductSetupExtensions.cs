namespace Web.Areas.Product.Services;

public static class ProductSetupExtensions
{
    public static IServiceCollection AddProducts(this IServiceCollection services)
    {
        services.AddScoped<ProductService>();

        return services;
    }
}