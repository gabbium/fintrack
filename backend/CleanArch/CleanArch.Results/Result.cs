namespace CleanArch.Results;

public class Result<T>(T? value, bool isSuccess, Error? error) : Result(isSuccess, error)
{
    public T? Value => value;

    public static Result<T> Success(T value) => new(value, true, null);

    public static new Result<T> Failure(Error error) => new(default, false, error);

    public static implicit operator Result<T>(T value) => Success(value);

    public static implicit operator Result<T>(Error error) => Failure(error);
}
