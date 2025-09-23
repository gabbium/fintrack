using Fintrack.Ledger.Application.Commands.UpdateMovement;
using Fintrack.Ledger.Application.UnitTests.TestHelpers.Builders;

namespace Fintrack.Ledger.Application.UnitTests.Commands.UpdateMovement;

public class UpdateMovementCommandValidatorTests
{
    private readonly UpdateMovementCommandValidator _validator = new();

    [Fact]
    public void Validate_WhenCommandIsValid_ThenHasNoValidationErrors()
    {
        // Arrange
        var command = new UpdateMovementCommandBuilder().Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenIdIsEmpty_ThenHasValidationError()
    {
        // Arrange
        var command = new UpdateMovementCommandBuilder()
            .WithId(Guid.Empty)
            .Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_WhenAmountIsZeroOrNegative_ThenHasValidationError(decimal invalidAmount)
    {
        // Arrange
        var command = new UpdateMovementCommandBuilder()
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
        var command = new UpdateMovementCommandBuilder()
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
        var command = new UpdateMovementCommandBuilder()
            .WithDescription(new string('a', 129))
            .Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Description);
    }
}
