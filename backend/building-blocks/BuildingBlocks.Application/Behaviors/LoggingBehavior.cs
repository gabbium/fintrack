namespace BuildingBlocks.Application.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IMediatorBehavior<TRequest, TResponse>
{
    public async Task<TResponse> HandleAsync(
        TRequest request,
        Func<Task<TResponse>> next,
        CancellationToken cancellationToken = default)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling {RequestName} {@Request}",
            requestName,
            request);

        var sw = Stopwatch.StartNew();

        var response = await next();

        logger.LogInformation("Handled {RequestName} with {@Response} in {Ms} ms",
            requestName,
            response,
            sw.ElapsedMilliseconds);

        sw.Stop();

        return response;
    }
}
