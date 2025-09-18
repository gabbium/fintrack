using Fintrack.Identity.Domain.UnitTests.TestHelpers.Builders;

namespace Fintrack.Identity.Domain.UnitTests.Entities;

public class UserTests
{
    [Fact]
    public void Ctor_WhenValidArguments_ThenCreatesUser()
    {
        // Arrange
        var email = "user@example.com";

        // Act
        var user = new UserBuilder()
            .WithEmail(email)
            .Build();

        // Assert
        user.Email.ShouldBe(email);
    }
}
