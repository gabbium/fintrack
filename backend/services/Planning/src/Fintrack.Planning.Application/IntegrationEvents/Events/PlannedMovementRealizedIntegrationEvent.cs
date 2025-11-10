using BuildingBlocks.Application.EventBus;
using Fintrack.Planning.Domain.PlannedMovementAggregate;
using Fintrack.Planning.Domain.PlannedMovementAggregate.Events;

namespace Fintrack.Planning.Application.IntegrationEvents.Events;

public sealed record PlannedMovementRealizedIntegrationEvent(
    Guid PlannedMovementId,
    Guid UserId,
    PlannedMovementKind Kind,
    decimal Amount,
    string? Description)
    : IntegrationEvent
{
    public static PlannedMovementRealizedIntegrationEvent FromDomainEvent(PlannedMovementRealizedDomainEvent domainEvent)
    {
        return new(domainEvent.PlannedMovementId,
            domainEvent.UserId,
            domainEvent.Kind,
            domainEvent.Amount,
            domainEvent.Description);
    }
}
