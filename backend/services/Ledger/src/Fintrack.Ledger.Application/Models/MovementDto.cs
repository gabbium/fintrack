using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Application.Models;

public sealed record MovementDto
{
    [Description("Unique identifier of the movement.")]
    public Guid Id { get; init; }

    [Description("Type of the movement.")]
    public MovementKind Kind { get; init; }

    [Description("Monetary value of the movement.")]
    public decimal Amount { get; init; }

    [Description("Optional description for the movement (e.g., 'Lunch', 'Salary').")]
    public string? Description { get; init; }

    [Description("Date and time when the movement occurred.")]
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
