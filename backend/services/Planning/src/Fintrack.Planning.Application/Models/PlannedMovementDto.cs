using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.Models;

public sealed record PlannedMovementDto
{
    [Description("Unique identifier of the planned movement.")]
    public Guid Id { get; init; }

    [Description("Type of the planned movement.")]
    public PlannedMovementKind Kind { get; init; }

    [Description("Monetary value planned for this movement.")]
    public decimal Amount { get; init; }

    [Description("Optional description for the planned movement (e.g., 'Rent', 'Bonus').")]
    public string? Description { get; init; }

    [Description("Date when the movement is due (expected to occur).")]
    public DateTimeOffset DueOn { get; init; }

    [Description("Current status of the planned movement (Active, Canceled, Realized, Overdue).")]
    public PlannedMovementStatus Status { get; init; }

    public static PlannedMovementDto FromAggregate(PlannedMovement plannedMovement)
    {
        return new()
        {
            Id = plannedMovement.Id,
            Kind = plannedMovement.Kind,
            Amount = plannedMovement.Amount,
            Description = plannedMovement.Description,
            DueOn = plannedMovement.DueOn,
            Status = plannedMovement.Status
        };
    }
}
