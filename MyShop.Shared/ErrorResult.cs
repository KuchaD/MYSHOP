namespace MyShop.Domain;

public class ErrorResult
{
    public string Message { get; set; }

    public string Code { get; set; }

    public ErrorResult(string message, string code)
    {
        Message = message;
        Code = code;
    }



}