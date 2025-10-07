using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Api.Models;

public sealed class ListPlannedMovementsRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    [DefaultValue(1)]
    [Description("Page number.")]
    public int PageNumber { get; init; }

    [Required]
    [Range(1, 100)]
    [DefaultValue(20)]
    [Description("Items per page.")]
    public int PageSize { get; init; }

    [Description("Sort order, e.g. 'dueOn desc' or 'amount asc'.")]
    public string? Order { get; init; }

    [Description("Filter by planned movement kind.")]
    public PlannedMovementKind[]? Kind { get; init; }

    [Description("Filter by planned movement status.")]
    public PlannedMovementStatus[]? Status { get; init; }

    [Description("Filter by minimum due date (inclusive).")]
    public DateTimeOffset? MinDueOn { get; init; }

    [Description("Filter by maximum due date (inclusive).")]
    public DateTimeOffset? MaxDueOn { get; init; }
}
