namespace Fintrack.ServiceDefaults.ProblemDetail;

public sealed class DefaultExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var error = Error.Failure("An unexpected error occurred");

        var problem = CustomResults.Problem(error);

        await problem.ExecuteAsync(httpContext);

        return true;
    }
}
