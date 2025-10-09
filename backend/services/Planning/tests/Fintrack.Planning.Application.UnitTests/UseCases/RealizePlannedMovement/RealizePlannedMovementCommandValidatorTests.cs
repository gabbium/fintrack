using Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Planning.Application.UseCases.RealizePlannedMovement;

namespace Fintrack.Planning.Application.UnitTests.UseCases.RealizePlannedMovement;

public class RealizePlannedMovementCommandValidatorTests
{
    private readonly RealizePlannedMovementCommandValidator _validator = new();

    [Fact]
    public void Validate_WhenCommandIsValid_ThenHasNoValidationErrors()
    {
        // Arrange
        var command = new RealizePlannedMovementCommandBuilder().Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenIdIsEmpty_ThenHasValidationError()
    {
        // Arrange
        var command = new RealizePlannedMovementCommandBuilder()
            .WithId(Guid.Empty)
            .Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }
}
