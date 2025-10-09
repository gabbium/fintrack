namespace Fintrack.Planning.Application.UseCases.RealizePlannedMovement;

internal sealed class RealizePlannedMovementCommandValidator
    : AbstractValidator<RealizePlannedMovementCommand>
{
    public RealizePlannedMovementCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}

