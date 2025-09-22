using MyShop.Product.ExternalServices.Dto;
using Refit;

namespace MyShop.Product;

public interface IShopApi
{
    [Get("/api/v1/checkout/payment/{gtin}")]
    Task<IApiResponse<ProductResponse>> GetPayment(
        string gtin,
        decimal tip,
        CancellationToken cancellationToken);
}