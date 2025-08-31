using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Product.ExternalServices;

namespace MyShop.Product;

public static class ServiceExtensions
{
    public static IServiceCollection AddProductModule(this IServiceCollection services)
    {
        services.AddScoped<IProductProxy, ProductProxy>();
        return services;
    }
}