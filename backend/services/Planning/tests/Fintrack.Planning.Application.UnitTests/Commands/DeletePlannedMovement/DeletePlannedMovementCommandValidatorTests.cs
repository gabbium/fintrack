using Fintrack.Planning.Application.Commands.DeletePlannedMovement;
using Fintrack.Planning.Application.UnitTests.TestSupport.Builders;

namespace Fintrack.Planning.Application.UnitTests.Commands.DeletePlannedMovement;

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
