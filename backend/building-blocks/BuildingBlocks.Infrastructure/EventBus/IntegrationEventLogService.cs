using BuildingBlocks.Application.EventBus;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EventBus;

public class IntegrationEventLogService<TContext>(TContext context) : IIntegrationEventLogService
    where TContext : DbContext
{
    public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<IntegrationEventLogEntry>()
            .Where(eventLog => eventLog.State == EventStateEnum.NotPublished)
            .OrderBy(eventLog => eventLog.CreatedAt)
            .Take(20)
            .ToListAsync(cancellationToken);
    }

    public async Task AddEventAsync(IntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        var integrationEventLogEntry = new IntegrationEventLogEntry(integrationEvent);
        await context.Set<IntegrationEventLogEntry>().AddAsync(integrationEventLogEntry, cancellationToken);
    }

    public Task MarkEventAsPublishedAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return UpdateEventStatus(eventId, EventStateEnum.Published, cancellationToken);
    }

    public Task MarkEventAsInProgressAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return UpdateEventStatus(eventId, EventStateEnum.InProgress, cancellationToken);
    }

    public Task MarkEventAsFailedAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return UpdateEventStatus(eventId, EventStateEnum.PublishedFailed, cancellationToken);
    }

    private Task<int> UpdateEventStatus(Guid eventId, EventStateEnum status, CancellationToken cancellationToken = default)
    {
        var eventLogEntry = context.Set<IntegrationEventLogEntry>()
            .Single(eventLog => eventLog.EventId == eventId);

        eventLogEntry.State = status;

        return context.SaveChangesAsync(cancellationToken);
    }
}
