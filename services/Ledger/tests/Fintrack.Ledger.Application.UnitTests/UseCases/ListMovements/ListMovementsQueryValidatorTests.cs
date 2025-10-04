using Fintrack.Ledger.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Ledger.Application.UseCases.ListMovements;

namespace Fintrack.Ledger.Application.UnitTests.UseCases.ListMovements;

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
    [InlineData("occurredon asc")]
    [InlineData("occurredon desc")]
    [InlineData("amount asc")]
    [InlineData("amount desc")]
    public void Validate_WhenQueryWithOrderIsValid_ThenHasNoValidationError(string validOrder)
    {
        // Arrange
        var query = new ListMovementsQueryBuilder()
            .WithOrder(validOrder)
            .Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(q => q.Order);
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


    [Fact]
    public void Validate_WhenOrderIsInvalid_ThenHasValidationError()
    {
        // Arrange
        var query = new ListMovementsQueryBuilder()
            .WithOrder("invalid stuff")
            .Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Order);
    }

    [Fact]
    public void Validate_WhenMinOccurredOnIsGreaterThanMaxOccurredOn_ThenHasValidationError()
    {
        // Arrange
        var query = new ListMovementsQueryBuilder()
            .WithMinOccurredOn(new DateTimeOffset(2025, 10, 10, 0, 0, 0, TimeSpan.Zero))
            .WithMaxOccurredOn(new DateTimeOffset(2025, 10, 1, 0, 0, 0, TimeSpan.Zero))
            .Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.MinOccurredOn);
    }
}
