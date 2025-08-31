using System.Net;
using System.Runtime.CompilerServices;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Refit;

namespace MyShop.Domain;

internal static class ProxyHelper
{
    public static async Task<Result<Maybe<TOutput>>> GetResultMaybe<TResult, TOutput, TCaller>(
        Func<CancellationToken, Task<IApiResponse<TResult>>> apiCallFunc,
        ILogger<TCaller> logger,
        CancellationToken cancellationToken,
        bool threatNotFoundAsEmpty = false,
        [CallerMemberName] string requestName = ""
        )
        where TResult : IResponseMapper<TOutput>
        where TOutput : class
        where TCaller : class
    {
        var response = await apiCallFunc(cancellationToken);
        if (response.IsSuccessful)
        {
            return Maybe.From(response.Content.MapToOutput());
        }
        if (threatNotFoundAsEmpty && response.StatusCode == HttpStatusCode.NotFound)
        {
            logger.LogInformation("[{RequestName}] downstream API call returned NotFound and is threated as empty.", requestName);

            return Maybe<TOutput>.None;
        }

        var error = response.Error.ToDownstreamError(requestName);

        // succesfull response but content deserialization failed
        if (response.IsSuccessStatusCode)
        {
            logger.LogError(response.Error,
                "[{RequestName}] downstream API call passed but there was an error during deserialization [{DeserializationError}].",
                error.RequestName,
                error.Message);

            return Maybe<TOutput>.None;
        }

        logger.LogError(response.Error,
            "[{RequestName}] downstream API call failed with status [{StatusCode}] and error [{DownstreamError}].",
            error.RequestName,
            error.StatusCode,
            error.Message);

        return Result.Failure<Maybe<TOutput>>(error.Message);
    }

    public static async Task<Result<Maybe<TOutput>, DownstreamApiError>> GetDownstreamResultMaybe<TResult, TOutput, TCaller>(
        Func<CancellationToken, Task<IApiResponse<TResult>>> apiCallFunc,
        ILogger<TCaller> logger,
        CancellationToken cancellationToken,
        bool threatNotFoundAsEmpty = false,
        [CallerMemberName] string requestName = ""
        )
        where TResult : IResponseMapper<TOutput>
        where TOutput : class
        where TCaller : class
    {
        var response = await apiCallFunc(cancellationToken);
        if (response.IsSuccessful)
        {
            return Maybe.From(response.Content.MapToOutput());
        }
        if (threatNotFoundAsEmpty && response.StatusCode == HttpStatusCode.NotFound)
        {
            logger.LogInformation("[{RequestName}] downstream API call returned NotFound and is threated as empty.", requestName);

            return Maybe<TOutput>.None;
        }

        var error = response.Error.ToDownstreamError(requestName);

        // succesfull response but content deserialization failed
        if (response.IsSuccessStatusCode)
        {
            logger.LogError(response.Error,
                "[{RequestName}] downstream API call passed but there was an error during deserialization [{DeserializationError}].",
                error.RequestName,
                error.Message);

            return Maybe<TOutput>.None;
        }

        logger.LogError(response.Error,
            "[{RequestName}] downstream API call failed with status [{StatusCode}] and error [{DownstreamError}].",
            error.RequestName,
            error.StatusCode,
            error.Message);

        return error;
    }

    public static async Task<Maybe<DownstreamApiError>> GetDownstreamMaybe<TCaller>(
        Func<CancellationToken, Task<IApiResponse>> apiCallFunc,
        ILogger<TCaller> logger,
        CancellationToken cancellationToken,
        [CallerMemberName] string requestName = ""
        )
        where TCaller : class
    {
        var response = await apiCallFunc(cancellationToken);
        if (response.IsSuccessful)
        {
            return Maybe.None;
        }

        var error = response.Error.ToDownstreamError(requestName);

        logger.LogError(response.Error,
            "[{RequestName}] downstream API call failed with status [{StatusCode}] and error [{DownstreamError}].",
            error.RequestName,
            error.StatusCode,
            error.Message);

        return error;
    }
}

