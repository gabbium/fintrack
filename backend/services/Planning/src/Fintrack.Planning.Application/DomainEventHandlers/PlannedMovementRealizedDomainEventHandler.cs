using Fintrack.Planning.Application.IntegrationEvents;
using Fintrack.Planning.Application.IntegrationEvents.Events;
using Fintrack.Planning.Domain.PlannedMovementAggregate.Events;

namespace Fintrack.Planning.Application.DomainEventHandlers;

internal sealed class PlannedMovementRealizedDomainEventHandler(
    IPlanningIntegrationEventService planningIntegrationEventService)
    : IDomainEventHandler<PlannedMovementRealizedDomainEvent>
{
    public async Task HandleAsync(
        PlannedMovementRealizedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        var integrationEvent = PlannedMovementRealizedIntegrationEvent.FromDomainEvent(domainEvent);
        await planningIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}
