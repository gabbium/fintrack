using Fintrack.Ledger.Application.Queries.GetMovementById;
using Fintrack.Ledger.Application.UnitTests.TestHelpers.Builders;

namespace Fintrack.Ledger.Application.UnitTests.Queries.GetMovementById;

public class GetMovementByIdValidatorTests
{
    private readonly GetMovementByIdValidator _validator = new();

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
