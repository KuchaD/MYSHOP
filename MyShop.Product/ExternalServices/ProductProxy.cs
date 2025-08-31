namespace MyShop.Product.ExternalServices;

public class ProductProxy : IProductProxy
{
    public Task<ProductDetail> GetProductByEan(string ean, decimal tip)
    {
        // Simulate fetching product details from an external service
        return Task.FromResult(new ProductDetail
        {
            Ean = ean,
            Name = "Sample Product",
            Price = 19.99m + tip,
            QRData = "SPD*1.0*ACC:CZ5855000000001234567899*AM:1.00*CC:CZK*MSG:Testovaci platba"
        });
    }
}