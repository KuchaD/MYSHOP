using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using MyShop.Domain;
using MyShop.Product.ExternalServices.Dto;

namespace MyShop.Product.ExternalServices;

public class ProductProxy(
    IShopApi shopApi,
    ILogger<ProductProxy> logger) : IProductProxy
{
    public async Task<Result<ProductDetail, ErrorResult>> GetProductByEan(string ean, decimal tip, CancellationToken ct = default)
    {
        var product = await ProxyHelper.GetDownstreamResultMaybe<ProductResponse, ProductDetail,ProductProxy>(async (cancellationToken) => await shopApi.GetPayment(ean, tip, cancellationToken),
            logger,
            ct);

        if (product.IsSuccess)
        {
            if (product.Value.HasValue)
            {
                return product.Value.Value;
            }

            return new ErrorResult("Product not found", nameof(ProductProxy));
        }

        logger.LogError("Failed to fetch product details for EAN: {Ean}. Error: {Error}", ean, product.Error);
        return new ErrorResult("Product not found", nameof(ProductProxy));
    }
}