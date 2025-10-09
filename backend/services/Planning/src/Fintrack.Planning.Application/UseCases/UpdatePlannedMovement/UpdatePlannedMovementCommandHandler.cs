using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UseCases.UpdatePlannedMovement;

internal sealed class UpdatePlannedMovementCommandHandler(
    IPlannedMovementRepository plannedMovementRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePlannedMovementCommand, PlannedMovementDto>
{
    public async Task<Result<PlannedMovementDto>> HandleAsync(
        UpdatePlannedMovementCommand command,
        CancellationToken cancellationToken = default)
    {
        var plannedMovement = await plannedMovementRepository.GetByIdAsync(command.Id, cancellationToken);

        if (plannedMovement is null)
        {
            return Error.NotFound("Planned movement was not found.");
        }

        if (plannedMovement.Status != PlannedMovementStatus.Active)
        {
            return Error.Business("Planned movement must be active to be modified.");
        }

        plannedMovement.ChangeKind(command.Kind);
        plannedMovement.ChangeAmount(command.Amount);
        plannedMovement.ChangeDescription(command.Description);
        plannedMovement.ChangeDueOn(command.DueOn);

        await plannedMovementRepository.UpdateAsync(plannedMovement, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return PlannedMovementDto.FromDomain(plannedMovement);
    }
}
