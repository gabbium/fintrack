namespace CleanArch.Results.AspNetCore;

public static class ResultExtensions
{
    public static IResult ToMinimalApiResult(this Result result, Func<IResult> onSuccess)
    {
        if (result.IsSuccess)
            return onSuccess();

        if (result.Error is null)
            throw new InvalidOperationException("Result is failure but Error is null.");

        return result.Error.ToMinimalApiResult();
    }

    private static IResult ToMinimalApiResult(this Error error)
    {
        return Microsoft.AspNetCore.Http.Results.Problem(
            title: GetTitle(error),
            detail: GetDetail(error),
            type: GetType(error.Type),
            statusCode: GetStatusCode(error.Type)
        );
    }

    private static string GetTitle(Error error) =>
        error.Type switch
        {
            ErrorType.Validation => "Bad Request",
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            ErrorType.Unauthorized => "Unauthorized",
            ErrorType.Forbidden => "Forbidden",
            ErrorType.Business => "Unprocessable Entity",
            _ => "Server failure"
        };

    private static string GetDetail(Error error) =>
        error.Type switch
        {
            ErrorType.Validation => error.Description,
            ErrorType.NotFound => error.Description,
            ErrorType.Conflict => error.Description,
            ErrorType.Unauthorized => error.Description,
            ErrorType.Forbidden => error.Description,
            ErrorType.Business => error.Description,
            _ => "An unexpected error occurred"
        };

    private static string GetType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "https://www.rfc-editor.org/rfc/rfc9110#name-400-bad-request",
            ErrorType.NotFound => "https://www.rfc-editor.org/rfc/rfc9110#name-404-not-found",
            ErrorType.Conflict => "https://www.rfc-editor.org/rfc/rfc9110#name-409-conflict",
            ErrorType.Unauthorized => "https://www.rfc-editor.org/rfc/rfc9110#name-401-unauthorized",
            ErrorType.Forbidden => "https://www.rfc-editor.org/rfc/rfc9110#name-403-forbidden",
            ErrorType.Business => "https://www.rfc-editor.org/rfc/rfc9110#name-422-unprocessable-content",
            _ => "https://www.rfc-editor.org/rfc/rfc9110#name-500-internal-server-error"
        };

    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Business => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
}
