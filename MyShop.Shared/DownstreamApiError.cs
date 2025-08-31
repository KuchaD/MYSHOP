namespace MyShop.Domain;

public sealed class DownstreamApiError
{
    public string RequestName { get; }

    public int StatusCode { get; }

    public string Message { get; }

    public string Detail { get; }

    public DownstreamApiError(int statusCode, string message, string requestName = "")
    {
        StatusCode = statusCode;
        Message = message;
        RequestName = requestName;

        if (string.IsNullOrWhiteSpace(requestName))
        {
            Detail = $"Downstream - {Message}";
        }
        else
        {
            Detail = $"Downstream - {RequestName} - {Message}";
        }
    }
}
