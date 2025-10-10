namespace CleanArch.Mediator;

public sealed class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public Task<TResponse> SendAsync<TResponse>(
        ICommand<TResponse> command,
        CancellationToken cancellationToken = default)
    {
        var requestType = command.GetType();
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        dynamic handler = serviceProvider.GetRequiredService(handlerType);
        var handlerMethod = handlerType.GetMethod(nameof(ICommandHandler<ICommand<TResponse>, TResponse>.HandleAsync))!;

        Func<Task<TResponse>> next = () =>
        {
            return (Task<TResponse>)handlerMethod.Invoke(handler, new object?[] { command, cancellationToken });
        };

        var behaviorType = typeof(IMediatorBehavior<,>).MakeGenericType(requestType, typeof(TResponse));
        var behaviors = serviceProvider.GetServices(behaviorType).Reverse().ToArray();
        var behaviorMethod = behaviorType.GetMethod(nameof(IMediatorBehavior<ICommand<TResponse>, TResponse>.HandleAsync))!;

        foreach (var behavior in behaviors)
        {
            var current = next;
            next = () => (Task<TResponse>)behaviorMethod.Invoke(behavior, [command, current, cancellationToken])!;
        }

        return next();
    }

    public Task<TResponse> SendAsync<TResponse>(
        IQuery<TResponse> query,
        CancellationToken cancellationToken = default)
    {
        var requestType = query.GetType();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        dynamic handler = serviceProvider.GetRequiredService(handlerType);
        var handlerMethod = handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResponse>, TResponse>.HandleAsync))!;

        Func<Task<TResponse>> next = () =>
        {
            return (Task<TResponse>)handlerMethod.Invoke(handler, new object?[] { query, cancellationToken });
        };

        var behaviorType = typeof(IMediatorBehavior<,>).MakeGenericType(requestType, typeof(TResponse));
        var behaviors = serviceProvider.GetServices(behaviorType).Reverse().ToArray();
        var behaviorMethod = behaviorType.GetMethod(nameof(IMediatorBehavior<IQuery<TResponse>, TResponse>.HandleAsync))!;

        foreach (var behavior in behaviors)
        {
            var current = next;
            next = () => (Task<TResponse>)behaviorMethod.Invoke(behavior, [query, current, cancellationToken])!;
        }

        return next();
    }

    public async Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var eventType = domainEvent.GetType();
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
        var handlers = serviceProvider.GetServices(handlerType);

        var method = handlerType.GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))!;

        foreach (var handler in handlers)
        {
            await (Task)method.Invoke(handler, [domainEvent, cancellationToken])!;
        }
    }
}
