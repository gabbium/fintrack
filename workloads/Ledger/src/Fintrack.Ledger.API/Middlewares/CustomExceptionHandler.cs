namespace Fintrack.Ledger.API.Middlewares;

public sealed class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        Error error = exception switch
        {
            _ => Error.Failure("An unexpected error occurred"),
        };

        var result = CustomResults.Problem(error);

        await result.ExecuteAsync(httpContext);

        return true;
    }
}
