using CSharpFunctionalExtensions;
using MyShop.Domain;

namespace MyShop.Product;

public interface IProductProxy
{
    public Task<Result<ProductDetail, ErrorResult>> GetProductByEan(string ean, decimal tip, CancellationToken ct);
}