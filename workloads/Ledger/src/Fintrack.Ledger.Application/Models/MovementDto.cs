using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.Application.Models;

public sealed record MovementDto
{
    public Guid Id { get; init; }
    public MovementKind Kind { get; init; }
    public decimal Amount { get; init; }
    public string? Description { get; init; }
    public DateTimeOffset OccurredOn { get; init; }

    public static MovementDto FromDomain(Movement movement)
    {
        return new()
        {
            Id = movement.Id,
            Kind = movement.Kind,
            Amount = movement.Amount,
            Description = movement.Description,
            OccurredOn = movement.OccurredOn
        };
    }
}
