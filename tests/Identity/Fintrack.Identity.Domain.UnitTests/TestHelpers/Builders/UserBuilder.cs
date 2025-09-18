using Fintrack.Identity.Domain.Entities;

namespace Fintrack.Identity.Domain.UnitTests.TestHelpers.Builders;

public class UserBuilder
{
    private string _email = "user@example";

    public UserBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public User Build()
    {
        return new(_email);
    }
}
