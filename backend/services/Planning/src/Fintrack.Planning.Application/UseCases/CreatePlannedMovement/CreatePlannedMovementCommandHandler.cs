using Fintrack.Planning.Application.Interfaces;
using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UseCases.CreatePlannedMovement;

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

        return PlannedMovementDto.FromDomain(movement);
    }
}
