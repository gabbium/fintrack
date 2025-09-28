using Fintrack.Ledger.Application.Queries.ListMovements;
using Fintrack.Ledger.Application.UnitTests.TestHelpers.Builders;

namespace Fintrack.Ledger.Application.UnitTests.Queries.ListMovements;

public class ListMovementsQueryValidatorTests
{
    private readonly ListMovementsQueryValidator _validator = new();

    [Fact]
    public void Validate_WhenQueryIsValid_ThenHasNoValidationErrors()
    {
        // Arrange
        var query = new ListMovementsQueryBuilder().Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_WhenPageNumberIsZeroOrNegative_ThenHasValidationError(int invalidPageNumber)
    {
        // Arrange
        var query = new ListMovementsQueryBuilder()
            .WithPageNumber(invalidPageNumber)
            .Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.PageNumber);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_WhenPageSizeIsZeroOrNegative_ThenHasValidationError(int invalidPageSize)
    {
        // Arrange
        var query = new ListMovementsQueryBuilder()
            .WithPageSize(invalidPageSize)
            .Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.PageSize);
    }

    [Fact]
    public void Validate_WhenPageSizeExceedsMax_ThenHasValidationError()
    {
        // Arrange
        var query = new ListMovementsQueryBuilder()
            .WithPageSize(101)
            .Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.PageSize);
    }
}
