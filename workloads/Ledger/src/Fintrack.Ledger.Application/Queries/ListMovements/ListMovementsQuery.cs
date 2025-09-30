using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.Application.Queries.ListMovements;

public sealed record ListMovementsQuery(
    int PageNumber,
    int PageSize,
    List<MovementKind> Kinds,
    DateTimeOffset? MinOccurredOn,
    DateTimeOffset? MaxOccurredOn)
    : IQuery<PaginatedList<MovementDto>>;
