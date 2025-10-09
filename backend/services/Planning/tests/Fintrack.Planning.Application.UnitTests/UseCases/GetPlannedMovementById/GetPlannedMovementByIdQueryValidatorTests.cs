using Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Planning.Application.UseCases.GetPlannedMovementById;

namespace Fintrack.Planning.Application.UnitTests.UseCases.GetPlannedMovementById;

public class GetPlannedMovementByIdQueryValidatorTests
{
    private readonly GetPlannedMovementByIdQueryValidator _validator = new();

    [Fact]
    public void Validate_WhenQueryIsValid_ThenHasNoValidationErrors()
    {
        // Arrange
        var query = new GetPlannedMovementByIdQueryBuilder().Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenIdIsEmpty_ThenHasValidationError()
    {
        // Arrange
        var query = new GetPlannedMovementByIdQueryBuilder()
            .WithId(Guid.Empty)
            .Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Id);
    }
}
