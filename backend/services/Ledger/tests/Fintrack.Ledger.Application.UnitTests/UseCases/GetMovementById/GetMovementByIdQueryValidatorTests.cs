using Fintrack.Ledger.Application.UnitTests.TestSupport.Builders;
using Fintrack.Ledger.Application.UseCases.GetMovementById;

namespace Fintrack.Ledger.Application.UnitTests.UseCases.GetMovementById;

public class GetMovementByIdQueryValidatorTests
{
    private readonly GetMovementByIdQueryValidator _validator = new();

    [Fact]
    public void Validate_WhenQueryIsValid_ThenHasNoValidationErrors()
    {
        // Arrange
        var query = new GetMovementByIdQueryBuilder().Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenIdIsEmpty_ThenHasValidationError()
    {
        // Arrange
        var query = new GetMovementByIdQueryBuilder()
            .WithId(Guid.Empty)
            .Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Id);
    }
}
