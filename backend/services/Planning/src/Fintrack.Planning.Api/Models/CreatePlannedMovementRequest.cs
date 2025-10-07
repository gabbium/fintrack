using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Api.Models;

public sealed class CreatePlannedMovementRequest
{
    [Required]
    [Description("Type of the planned movement.")]
    public PlannedMovementKind Kind { get; init; }

    [Required]
    [Range(0.01, double.MaxValue)]
    [Description("Monetary value planned for this movement.")]
    public decimal Amount { get; init; }

    [MaxLength(128)]
    [Description("Optional description for the planned movement (e.g., 'Rent', 'Bonus').")]
    public string? Description { get; init; }

    [Required]
    [Description("Date when the movement is due (expected to occur).")]
    public DateTimeOffset DueOn { get; init; }
}
