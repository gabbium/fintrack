using Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Planning.Application.UseCases.UpdatePlannedMovement;

namespace Fintrack.Planning.Application.UnitTests.UseCases.UpdatePlannedMovement;

public class UpdatePlannedMovementCommandValidatorTests
{
    private readonly UpdatePlannedMovementCommandValidator _validator = new();

    [Fact]
    public void Validate_WhenCommandIsValid_ThenHasNoValidationErrors()
    {
        // Arrange
        var command = new UpdatePlannedMovementCommandBuilder().Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenIdIsEmpty_ThenHasValidationError()
    {
        // Arrange
        var command = new UpdatePlannedMovementCommandBuilder()
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
        var command = new UpdatePlannedMovementCommandBuilder()
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
        var command = new UpdatePlannedMovementCommandBuilder()
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
        var command = new UpdatePlannedMovementCommandBuilder()
            .WithDescription(new string('a', 129))
            .Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Description);
    }
}
