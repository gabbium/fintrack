namespace Fintrack.Planning.Application.Commands.RealizePlannedMovement;

internal sealed class RealizePlannedMovementCommandValidator
    : AbstractValidator<RealizePlannedMovementCommand>
{
    public RealizePlannedMovementCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}

