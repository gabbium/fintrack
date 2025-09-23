using Fintrack.Identity.Application.Models;
using Fintrack.Identity.Domain.UnitTests.TestHelpers.Builders;

namespace Fintrack.Identity.Application.UnitTests.Models;

public class AuthDtoTests
{
    [Fact]
    public void FromDomain_MapsAllPropertiesCorrectly()
    {
        // Arrange
        var user = new UserBuilder().Build();
        var expectedToken = "jwt-token-123";

        // Act
        var model = AuthDto.FromDomain(user, expectedToken);

        // Assert
        model.AccessToken.ShouldBe(expectedToken);
        model.User.Id.ShouldBe(user.Id);
        model.User.Email.ShouldBe(user.Email);
    }
}
