using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.Application.Queries.ListMovements;

public sealed record ListMovementsQuery(
    int PageNumber,
    int PageSize)
    : IQuery<PaginatedList<MovementDto>>;
