namespace MyShop.Product;

public interface IProductProxy
{
    public Task<ProductDetail> GetProductByEan(string ean, decimal tip);
}