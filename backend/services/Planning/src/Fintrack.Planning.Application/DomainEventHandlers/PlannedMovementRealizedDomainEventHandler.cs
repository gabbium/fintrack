using Fintrack.Planning.Domain.PlannedMovementAggregate.Events;

namespace Fintrack.Planning.Application.DomainEventHandlers;

internal sealed class PlannedMovementRealizedDomainEventHandler(
    ILogger<PlannedMovementRealizedDomainEventHandler> logger)
    : IDomainEventHandler<PlannedMovementRealizedDomainEvent>
{
    public Task HandleAsync(
        PlannedMovementRealizedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Planned movement with ID {PlannedMovementId} was realized.",
            domainEvent.PlannedMovementId);

        return Task.CompletedTask;
    }
}
