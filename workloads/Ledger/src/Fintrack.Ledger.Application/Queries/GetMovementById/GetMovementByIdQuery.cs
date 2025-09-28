using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.Application.Queries.GetMovementById;

public sealed record GetMovementByIdQuery(
    Guid Id)
    : IQuery<MovementDto>;
