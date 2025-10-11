using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UseCases.CancelPlannedMovement;

internal sealed class CancelPlannedMovementCommandHandler(
    IPlannedMovementRepository plannedMovementRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CancelPlannedMovementCommand, Result>
{
    public async Task<Result> HandleAsync(
        CancelPlannedMovementCommand command,
        CancellationToken cancellationToken = default)
    {
        var plannedMovement = await plannedMovementRepository.GetByIdAsync(command.Id, cancellationToken);

        if (plannedMovement is null)
        {
            return Error.NotFound("Planned movement was not found.");
        }

        if (plannedMovement.Status != PlannedMovementStatus.Active)
        {
            return Error.Business("Planned movement must be active to be canceled.");
        }

        plannedMovement.Cancel();

        await plannedMovementRepository.UpdateAsync(plannedMovement, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

