namespace Fintrack.Planning.Domain.PlannedMovementAggregate.Events;

public sealed class PlannedMovementRealizedDomainEvent(
    Guid plannedMovementId,
    Guid userId,
    PlannedMovementKind kind,
    decimal amount,
    string? description)
    : DomainEventBase
{
    public Guid PlannedMovementId { get; } = plannedMovementId;
    public Guid UserId { get; } = userId;
    public PlannedMovementKind Kind { get; } = kind;
    public decimal Amount { get; } = amount;
    public string? Description { get; } = description;

    public static PlannedMovementRealizedDomainEvent FromAggregate(PlannedMovement plannedMovement)
    {
        return new(plannedMovement.Id,
            plannedMovement.UserId,
            plannedMovement.Kind,
            plannedMovement.Amount,
            plannedMovement.Description);
    }
}
