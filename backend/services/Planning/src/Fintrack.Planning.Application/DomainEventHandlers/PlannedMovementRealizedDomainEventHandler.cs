using BuildingBlocks.Application.EventBus;
using Fintrack.Planning.Application.IntegrationEvents.Events;
using Fintrack.Planning.Domain.PlannedMovementAggregate.Events;

namespace Fintrack.Planning.Application.DomainEventHandlers;

internal sealed class PlannedMovementRealizedDomainEventHandler(
    IIntegrationEventLogService eventLogService,
    ILogger<PlannedMovementRealizedDomainEventHandler> logger)
    : IDomainEventHandler<PlannedMovementRealizedDomainEvent>
{
    public async Task HandleAsync(
        PlannedMovementRealizedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        var integrationEvent = PlannedMovementRealizedIntegrationEvent.FromDomainEvent(domainEvent);
        logger.LogInformation("Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", integrationEvent.Id, integrationEvent);
        await eventLogService.AddEventAsync(integrationEvent);
    }
}
