namespace Fintrack.Planning.Application.UseCases.CreatePlannedMovement;

internal sealed class CreatePlannedMovementCommandValidator
    : AbstractValidator<CreatePlannedMovementCommand>
{
    public CreatePlannedMovementCommandValidator()
    {
        RuleFor(command => command.Amount)
            .GreaterThan(0)
            .PrecisionScale(18, 2, true);

        RuleFor(command => command.Description)
            .MaximumLength(128);
    }
}
