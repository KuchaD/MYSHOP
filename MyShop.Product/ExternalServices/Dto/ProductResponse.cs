using MyShop.Domain;

namespace MyShop.Product.ExternalServices.Dto;

public class ProductResponse : IResponseMapper<ProductDetail>
{
    public string Ean { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public string Spayd { get; set; }

    public ProductDetail MapToOutput()
    {
        return new ProductDetail()
        {
            Ean = Ean,
            Name = Name,
            Price = Price
        };
    }
}