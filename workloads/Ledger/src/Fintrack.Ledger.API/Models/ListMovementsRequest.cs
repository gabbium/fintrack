namespace Fintrack.Ledger.API.Models;

public sealed class ListMovementsRequest
{
    [DefaultValue(1)]
    public int PageNumber { get; init; }

    [DefaultValue(20)]
    public int PageSize { get; init; }
}
