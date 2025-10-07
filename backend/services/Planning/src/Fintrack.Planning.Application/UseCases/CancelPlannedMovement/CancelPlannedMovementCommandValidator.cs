namespace Fintrack.Planning.Application.UseCases.CancelPlannedMovement;

internal sealed class CancelPlannedMovementCommandValidator
    : AbstractValidator<CancelPlannedMovementCommand>
{
    public CancelPlannedMovementCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}

