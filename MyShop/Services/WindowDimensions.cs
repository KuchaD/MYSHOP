namespace MyShop.Services;

public class WindowDimensions
{
    public int Width { get; set; }

    public int Height { get; set; }

    public bool IsSmall =>
        Width <= 480;

}