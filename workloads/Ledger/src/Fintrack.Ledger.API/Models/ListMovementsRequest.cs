using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.API.Models;

public sealed class ListMovementsRequest
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

    [Description("Filter by movement kind.")]
    public MovementKind[]? Kind { get; init; }

    [Description("Filter by minimum occurrence date (inclusive).")]
    public DateTimeOffset? MinOccurredOn { get; init; }

    [Description("Filter by maximum occurrence date (inclusive).")]
    public DateTimeOffset? MaxOccurredOn { get; init; }
}
