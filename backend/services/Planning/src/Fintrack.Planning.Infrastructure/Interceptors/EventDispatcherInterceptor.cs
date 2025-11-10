namespace Fintrack.Planning.Infrastructure.Interceptors;

internal sealed class EventDispatchInterceptor(
    IDomainEventDispatcher domainEventDispatcher)
    : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context is not PlanningDbContext planningContext)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var entitiesWithEvents = planningContext.ChangeTracker
            .Entries<HasDomainEventsBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count != 0)
            .ToArray();

        await domainEventDispatcher.DispatchAndClearEvents(entitiesWithEvents, cancellationToken);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
