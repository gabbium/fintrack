namespace BuildingBlocks.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IMediatorBehavior<TRequest, TResponse>
{
    private readonly IValidator<TRequest>[] _validators = [.. validators];

    public async Task<TResponse> HandleAsync(
        TRequest request,
        Func<Task<TResponse>> next,
        CancellationToken cancellationToken = default)
    {
        if (_validators.Length != 0)
        {
            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Count != 0)
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Count != 0)
                throw new ValidationException(failures);
        }

        return await next();
    }
}
