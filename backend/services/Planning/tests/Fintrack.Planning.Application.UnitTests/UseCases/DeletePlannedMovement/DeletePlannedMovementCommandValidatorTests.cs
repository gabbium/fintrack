using Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Planning.Application.UseCases.DeletePlannedMovement;

namespace Fintrack.Planning.Application.UnitTests.UseCases.DeletePlannedMovement;

public class DeletePlannedMovementCommandValidatorTests
{
    private readonly DeletePlannedMovementCommandValidator _validator = new();

    [Fact]
    public void Validate_WhenCommandIsValid_ThenHasNoValidationErrors()
    {
        // Arrange
        var command = new DeletePlannedMovementCommandBuilder().Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenIdIsEmpty_ThenHasValidationError()
    {
        // Arrange
        var command = new DeletePlannedMovementCommandBuilder()
            .WithId(Guid.Empty)
            .Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }
}
