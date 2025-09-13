namespace Fintrack.Ledger.Application.Commands.CreateMovement;

public class CreateMovementCommandHandler : ICommandHandler<CreateMovementCommand>
{
    public Task<Result> HandleAsync(CreateMovementCommand request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Success());
    }
}
