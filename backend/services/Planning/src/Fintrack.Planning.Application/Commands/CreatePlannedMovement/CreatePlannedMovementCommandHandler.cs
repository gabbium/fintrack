using BuildingBlocks.Application.Identity;
using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.Commands.CreatePlannedMovement;

internal sealed class CreatePlannedMovementCommandHandler(
    IIdentityService identityService,
    IPlannedMovementRepository plannedMovementRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePlannedMovementCommand, Result<PlannedMovementDto>>
{
    public async Task<Result<PlannedMovementDto>> HandleAsync(
        CreatePlannedMovementCommand command,
        CancellationToken cancellationToken = default)
    {
        var movement = new PlannedMovement(
            identityService.GetUserIdentity(),
            command.Kind,
            command.Amount,
            command.Description,
            command.DueOn,
            PlannedMovementStatus.Active);

        await plannedMovementRepository.AddAsync(movement, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return PlannedMovementDto.FromAggregate(movement);
    }
}
