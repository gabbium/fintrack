using Fintrack.Ledger.Application.UnitTests.TestSupport.Builders;
using Fintrack.Ledger.Application.UseCases.CreateMovement;

namespace Fintrack.Ledger.Application.UnitTests.UseCases.CreateMovement;

public class CreateMovementCommandValidatorTests
{
    private readonly CreateMovementCommandValidator _validator = new();

    [Fact]
    public void Validate_WhenCommandIsValid_ThenHasNoValidationErrors()
    {
        // Arrange
        var command = new CreateMovementCommandBuilder().Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_WhenAmountIsZeroOrNegative_ThenHasValidationError(decimal invalidAmount)
    {
        // Arrange
        var command = new CreateMovementCommandBuilder()
            .WithAmount(invalidAmount)
            .Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Amount);
    }

    [Fact]
    public void Validate_WhenAmountHasTooManyDecimals_ThenHasValidationError()
    {
        // Arrange
        var command = new CreateMovementCommandBuilder()
            .WithAmount(1.999m)
            .Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Amount);
    }

    [Fact]
    public void Validate_WhenDescriptionExceedsMaxLength_ThenHasValidationError()
    {
        // Arrange
        var command = new CreateMovementCommandBuilder()
            .WithDescription(new string('a', 129))
            .Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Description);
    }
}
