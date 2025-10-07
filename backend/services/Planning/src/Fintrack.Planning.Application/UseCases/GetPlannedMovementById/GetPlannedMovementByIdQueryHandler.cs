using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UseCases.GetPlannedMovementById;

internal sealed class GetPlannedMovementByIdQueryHandler(
    IPlannedMovementRepository plannedMovementRepository)
    : IQueryHandler<GetPlannedMovementByIdQuery, PlannedMovementDto>
{
    public async Task<Result<PlannedMovementDto>> HandleAsync(
        GetPlannedMovementByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var movement = await plannedMovementRepository.GetByIdAsync(query.Id, cancellationToken);

        if (movement is null)
        {
            return Error.NotFound("Planned movement was not found.");
        }

        return PlannedMovementDto.FromDomain(movement);
    }
}
