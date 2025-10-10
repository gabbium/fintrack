namespace CleanArch.Mediator.Primitives;

public interface IMediator
{
    Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
    Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
    Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}

