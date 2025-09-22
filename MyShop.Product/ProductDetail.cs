namespace MyShop.Product;

public record ProductDetail
{
    public string Ean { get; private set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string QRData { get; set; }

    public void WithEan(string ean) => Ean = ean;

}