namespace Fintrack.Identity.Application.Commands.PasswordlessLogin;

internal sealed class PasswordlessLoginCommandValidator
    : AbstractValidator<PasswordlessLoginCommand>
{
    public PasswordlessLoginCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);
    }
}
