namespace BuildingBlocks.Api.ProblemDetail;

public sealed class DefaultExceptionHandler : IExceptionHandler
{
    private readonly ILogger<DefaultExceptionHandler> _logger;
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

    public DefaultExceptionHandler(ILogger<DefaultExceptionHandler> logger)
    {
        _logger = logger;
        _exceptionHandlers = new()
        {
            { typeof(ValidationException), HandleValidationException },
        };
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();

        if (_exceptionHandlers.TryGetValue(exceptionType, out Func<HttpContext, Exception, Task>? value))
        {
            await value.Invoke(httpContext, exception);
            return true;
        }

        _logger.LogError(exception, "An unhandled exception has occurred while executing the request");

        var problem = Results.Problem(
            title: "Server failure",
            detail: "An unexpected error occurred",
            type: "https://www.rfc-editor.org/rfc/rfc9110#name-500-internal-server-error",
            statusCode: StatusCodes.Status500InternalServerError
        );

        await problem.ExecuteAsync(httpContext);

        return true;
    }

    private static async Task HandleValidationException(HttpContext httpContext, Exception ex)
    {
        var exception = (ValidationException)ex;

        var errors = exception.Errors.GroupBy(e => e.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(e => e.ErrorMessage).ToArray()
            );

        var problem = Results.ValidationProblem(
            errors: errors,
            title: "Bad Request",
            type: "https://www.rfc-editor.org/rfc/rfc9110#name-400-bad-request",
            statusCode: StatusCodes.Status400BadRequest
        );

        await problem.ExecuteAsync(httpContext);
    }
}
