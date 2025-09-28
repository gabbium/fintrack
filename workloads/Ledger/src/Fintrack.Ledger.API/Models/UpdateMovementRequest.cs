using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.API.Models;

public sealed class UpdateMovementRequest
{
    public MovementKind Kind { get; init; }
    public decimal Amount { get; init; }
    public string? Description { get; init; }
    public DateTimeOffset OccurredOn { get; init; }
}
