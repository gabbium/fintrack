namespace Fintrack.Ledger.Domain.Movements;

public sealed class Movement(
    Guid userId,
    MovementKind kind,
    decimal amount,
    string? description,
    DateTimeOffset occurredOn) : BaseEntity, IAggregateRoot
{
    public Guid UserId { get; private set; } = userId;
    public MovementKind Kind { get; private set; } = kind;
    public decimal Amount { get; private set; } = amount;
    public string? Description { get; private set; } = description;
    public DateTimeOffset OccurredOn { get; private set; } = occurredOn;
}

