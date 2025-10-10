namespace CleanArch.Results;

public record Error(ErrorType Type, string Description)
{
    public static Error Validation(string description) => new(ErrorType.Validation, description);
    public static Error NotFound(string description) => new(ErrorType.NotFound, description);
    public static Error Conflict(string description) => new(ErrorType.Conflict, description);
    public static Error Unauthorized(string description) => new(ErrorType.Unauthorized, description);
    public static Error Forbidden(string description) => new(ErrorType.Forbidden, description);
    public static Error Business(string description) => new(ErrorType.Business, description);
    public static Error Failure(string description) => new(ErrorType.Failure, description);
}
