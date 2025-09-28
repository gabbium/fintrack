namespace Fintrack.Ledger.Application.Commands.UpdateMovement;

internal sealed class UpdateMovementCommandValidator
    : AbstractValidator<UpdateMovementCommand>
{
    public UpdateMovementCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();

        RuleFor(c => c.Amount)
            .GreaterThan(0m)
            .PrecisionScale(18, 2, true);

        RuleFor(c => c.Description)
            .MaximumLength(128);
    }
}
