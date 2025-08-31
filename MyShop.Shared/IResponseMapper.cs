namespace MyShop.Domain;

public interface IResponseMapper<out TOutput>
{
    TOutput MapToOutput();
}