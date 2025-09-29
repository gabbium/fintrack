using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.API.Models;

public sealed class ListMovementsRequest
{
    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    [Description("Page number.")]
    public int PageNumber { get; init; }

    [DefaultValue(20)]
    [Range(1, 100)]
    [Description("Items per page.")]
    public int PageSize { get; init; }

    [Description("Filter by movement kind.")]
    public MovementKind[]? Kind { get; init; }
}
