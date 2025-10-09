using Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Planning.Application.UseCases.ListPlannedMovements;

namespace Fintrack.Planning.Application.UnitTests.UseCases.ListPlannedMovements;

public class ListPlannedMovementsQueryValidatorTests
{
    private readonly ListPlannedMovementsQueryValidator _validator = new();

    [Fact]
    public void Validate_WhenQueryIsValid_ThenHasNoValidationErrors()
    {
        // Arrange
        var query = new ListPlannedMovementsQueryBuilder().Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("dueon asc")]
    [InlineData("dueon desc")]
    [InlineData("amount asc")]
    [InlineData("amount desc")]
    public void Validate_WhenQueryWithOrderIsValid_ThenHasNoValidationError(string validOrder)
    {
        // Arrange
        var query = new ListPlannedMovementsQueryBuilder()
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
        var query = new ListPlannedMovementsQueryBuilder()
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
        var query = new ListPlannedMovementsQueryBuilder()
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
        var query = new ListPlannedMovementsQueryBuilder()
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
        var query = new ListPlannedMovementsQueryBuilder()
            .WithOrder("invalid stuff")
            .Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.Order);
    }

    [Fact]
    public void Validate_WhenMinDueOnIsGreaterThanMaxDueOn_ThenHasValidationError()
    {
        // Arrange
        var query = new ListPlannedMovementsQueryBuilder()
            .WithMinDueOn(new DateTimeOffset(2025, 10, 10, 0, 0, 0, TimeSpan.Zero))
            .WithMaxDueOn(new DateTimeOffset(2025, 10, 1, 0, 0, 0, TimeSpan.Zero))
            .Build();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.MinDueOn);
    }
}
