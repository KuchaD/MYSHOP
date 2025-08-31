using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using Refit;

namespace MyShop.Domain;

internal static partial class ApiExceptionExtensions
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    [GeneratedRegex(@"\s+")]
    private static partial Regex LinearizeRegex();

    public static DownstreamApiError ToDownstreamError(this ApiException exception, [CallerMemberName] string callerName = "")
    {
        if (exception is null)
        {
            return new DownstreamApiError((int)HttpStatusCode.InternalServerError, "Unknown error.", callerName);
        }

        if (!string.IsNullOrWhiteSpace(exception.Content))
        {
            try
            {
                var listOfErrors = new List<string>();

                var error = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, JsonOptions);
                if (error is not null)
                {
                    if (!string.IsNullOrWhiteSpace(error.Detail))
                    {
                        listOfErrors.Add(error.Detail);
                    }

                    if (error.Errors.Count > 0)
                    {
                        listOfErrors.AddRange(error.Errors.SelectMany(e => e.Value));
                    }
                }

                if (!string.IsNullOrWhiteSpace(exception.Message))
                {
                    listOfErrors.Add(exception.Message);
                }

                if (!string.IsNullOrWhiteSpace(exception.InnerException?.Message))
                {
                    listOfErrors.Add(exception.InnerException.Message);
                }

                if (listOfErrors.Count > 0)
                {
                    return new DownstreamApiError(GetStatusCode(exception.StatusCode),
                        $"{string.Join(" # ", listOfErrors)}", callerName);
                }

                return new DownstreamApiError(GetStatusCode(exception.StatusCode), $"{Linearize(exception.Content)}", callerName);
            }
            catch
            {
                // ignore and return default error
            }
        }

        return new DownstreamApiError(GetStatusCode(exception.StatusCode), "Unknown error.", callerName);
    }

    private static string Linearize(string input)
    {
        return LinearizeRegex().Replace(input, " ").Trim();
    }

    private static int GetStatusCode(HttpStatusCode statusCode)
    {
        return (int)statusCode;
    }
}