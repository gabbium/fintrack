namespace CleanArch.Mediator;

public sealed class DomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    public async Task DispatchAndClearEvents(IEnumerable<HasDomainEventsBase> entitiesWithEvents, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entitiesWithEvents)
        {
            var domainEvents = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                await mediator.PublishAsync(domainEvent, cancellationToken);
            }
        }
    }
}
