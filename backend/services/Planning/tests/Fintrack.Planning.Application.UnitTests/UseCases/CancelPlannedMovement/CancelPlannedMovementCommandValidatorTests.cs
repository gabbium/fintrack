using Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Planning.Application.UseCases.CancelPlannedMovement;

namespace Fintrack.Planning.Application.UnitTests.UseCases.CancelPlannedMovement;

public class CancelPlannedMovementCommandValidatorTests
{
    private readonly CancelPlannedMovementCommandValidator _validator = new();

    [Fact]
    public void Validate_WhenCommandIsValid_ThenHasNoValidationErrors()
    {
        // Arrange
        var command = new CancelPlannedMovementCommandBuilder().Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenIdIsEmpty_ThenHasValidationError()
    {
        // Arrange
        var command = new CancelPlannedMovementCommandBuilder()
            .WithId(Guid.Empty)
            .Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }
}
