using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UseCases.DeletePlannedMovement;

internal sealed class DeletePlannedMovementCommandHandler(
    IPlannedMovementRepository plannedMovementRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeletePlannedMovementCommand>
{
    public async Task<Result> HandleAsync(
        DeletePlannedMovementCommand command,
        CancellationToken cancellationToken = default)
    {
        var plannedMovement = await plannedMovementRepository.GetByIdAsync(command.Id, cancellationToken);

        if (plannedMovement is null)
        {
            return Result.Success();
        }

        if (plannedMovement.Status != PlannedMovementStatus.Active)
        {
            return Error.Business("Planned movement must be active to be deleted.");
        }

        await plannedMovementRepository.RemoveAsync(plannedMovement, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
