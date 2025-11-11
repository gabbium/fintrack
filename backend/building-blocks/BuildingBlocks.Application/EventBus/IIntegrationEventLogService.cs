namespace BuildingBlocks.Application.EventBus;

public interface IIntegrationEventLogService
{
    Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(CancellationToken cancellationToken = default);
    Task AddEventAsync(IntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
    Task MarkEventAsPublishedAsync(Guid eventId, CancellationToken cancellationToken = default);
    Task MarkEventAsInProgressAsync(Guid eventId, CancellationToken cancellationToken = default);
    Task MarkEventAsFailedAsync(Guid eventId, CancellationToken cancellationToken = default);
}
