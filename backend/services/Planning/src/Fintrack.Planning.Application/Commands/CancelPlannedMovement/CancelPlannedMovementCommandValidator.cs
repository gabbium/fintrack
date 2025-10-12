namespace Fintrack.Planning.Application.Commands.CancelPlannedMovement;

internal sealed class CancelPlannedMovementCommandValidator
    : AbstractValidator<CancelPlannedMovementCommand>
{
    public CancelPlannedMovementCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}

