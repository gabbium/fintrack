namespace Fintrack.Planning.Application.UseCases.UpdatePlannedMovement;

internal sealed class UpdatePlannedMovementCommandValidator
    : AbstractValidator<UpdatePlannedMovementCommand>
{
    public UpdatePlannedMovementCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();

        RuleFor(command => command.Amount)
            .GreaterThan(0)
            .PrecisionScale(18, 2, true);

        RuleFor(command => command.Description)
            .MaximumLength(128);
    }
}
