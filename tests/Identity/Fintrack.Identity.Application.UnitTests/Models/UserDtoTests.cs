using Fintrack.Identity.Application.Models;
using Fintrack.Identity.Domain.UnitTests.TestHelpers.Builders;

namespace Fintrack.Identity.Application.UnitTests.Models;

public class UserDtoTests
{
    [Fact]
    public void FromDomain_MapsAllPropertiesCorrectly()
    {
        // Arrange
        var user = new UserBuilder().Build();

        // Act
        var model = UserDto.FromDomain(user);

        // Assert
        model.Id.ShouldBe(user.Id);
        model.Email.ShouldBe(user.Email);
    }
}
