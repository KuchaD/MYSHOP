using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShop.Product.ExternalServices;
using Refit;

namespace MyShop.Product;

public static class ServiceExtensions
{
    public static IServiceCollection AddProductModule(this IServiceCollection services)
    {
        services.AddScoped<IProductProxy, ProductProxy>();
        services.AddRefitClient<IShopApi>()
            .ConfigureHttpClient((sp, client) =>
            {
                // var configuration = sp.GetRequiredService<IConfiguration>();
                // var baseUrl = configuration.GetValue<string>("DownstreamApis:ShopApi");
                client.BaseAddress = new Uri("https://shop.marekhanus.cz/");
            });
        return services;
    }
}