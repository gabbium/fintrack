using Fintrack.Ledger.Application.UnitTests.TestSupport.Builders;
using Fintrack.Ledger.Application.UseCases.DeleteMovement;

namespace Fintrack.Ledger.Application.UnitTests.UseCases.DeleteMovement;

public class DeleteMovementCommandValidatorTests
{
    private readonly DeleteMovementCommandValidator _validator = new();

    [Fact]
    public void Validate_WhenCommandIsValid_ThenHasNoValidationErrors()
    {
        // Arrange
        var command = new DeleteMovementCommandBuilder().Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenIdIsEmpty_ThenHasValidationError()
    {
        // Arrange
        var command = new DeleteMovementCommandBuilder()
            .WithId(Guid.Empty)
            .Build();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }
}
