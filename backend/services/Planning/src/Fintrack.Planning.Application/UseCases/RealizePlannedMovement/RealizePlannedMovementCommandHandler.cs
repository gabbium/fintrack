using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UseCases.RealizePlannedMovement;

internal sealed class RealizePlannedMovementCommandHandler(
    IPlannedMovementRepository plannedMovementRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RealizePlannedMovementCommand>
{
    public async Task<Result> HandleAsync(
        RealizePlannedMovementCommand command,
        CancellationToken cancellationToken = default)
    {
        var plannedMovement = await plannedMovementRepository.GetByIdAsync(command.Id, cancellationToken);

        if (plannedMovement is null)
        {
            return Error.NotFound("Planned movement was not found.");
        }

        if (plannedMovement.Status != PlannedMovementStatus.Active)
        {
            return Error.Business("Planned movement must be active to be realized.");
        }

        plannedMovement.Realize();

        await plannedMovementRepository.UpdateAsync(plannedMovement, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

