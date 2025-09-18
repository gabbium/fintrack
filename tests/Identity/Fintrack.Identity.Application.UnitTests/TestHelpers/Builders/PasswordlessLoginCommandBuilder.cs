using Fintrack.Identity.Application.Commands.PasswordlessLogin;

namespace Fintrack.Identity.Application.UnitTests.TestHelpers.Builders;

public class PasswordlessLoginCommandBuilder
{
    private string _email = "user@example";

    public PasswordlessLoginCommandBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public PasswordlessLoginCommand Build()
    {
        return new(_email);
    }
}
