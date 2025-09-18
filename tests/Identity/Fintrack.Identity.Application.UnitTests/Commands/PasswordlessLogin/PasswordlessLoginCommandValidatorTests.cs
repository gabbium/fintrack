using Fintrack.Identity.Application.Commands.PasswordlessLogin;
using Fintrack.Identity.Application.UnitTests.TestHelpers.Builders;

namespace Fintrack.Identity.Application.UnitTests.Commands.PasswordlessLogin;

public class PasswordlessLoginCommandValidatorTests
{
    private readonly PasswordlessLoginCommandValidator _validator = new();

    [Fact]
    public void Validate_WhenCommandIsValid_ThenHasNoValidationErrors()
    {
        // Arrange
        var command = new PasswordlessLoginCommandBuilder().Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenEmailIsEmpty_ThenHasValidationError()
    {
        // Arrange
        var command = new PasswordlessLoginCommandBuilder()
            .WithEmail(string.Empty)
            .Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Fact]
    public void Validate_WhenEmailIsInvalid_ThenHasValidationError()
    {
        // Arrange
        var command = new PasswordlessLoginCommandBuilder()
            .WithEmail("invalid-email")
            .Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Fact]
    public void Validate_WhenEmailExceedsMaxLength_ThenHasValidationError()
    {
        // Arrange
        var command = new PasswordlessLoginCommandBuilder()
            .WithEmail(new string('a', 257) + "@test.com")
            .Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }
}
