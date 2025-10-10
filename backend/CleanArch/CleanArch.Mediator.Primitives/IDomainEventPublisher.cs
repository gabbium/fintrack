namespace CleanArch.Mediator.Primitives;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(
        IEnumerable<HasDomainEventsBase> entitiesWithEvents,
        CancellationToken cancellationToken = default);
}
