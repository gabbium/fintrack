using Fintrack.Ledger.Domain.MovementAggregate;
using Fintrack.Ledger.Domain.UnitTests.TestHelpers.Builders;

namespace Fintrack.Ledger.Domain.UnitTests.MovementAggregate;

public class MovementTests
{
    [Fact]
    public void Ctor_WhenValidArguments_ThenCreatesMovement()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var kind = MovementKind.Income;
        var amount = 123.45m;
        var description = "Salary";
        var occurredOn = DateTimeOffset.UtcNow;

        // Act
        var movement = new MovementBuilder()
            .WithUserId(userId)
            .WithKind(kind)
            .WithAmount(amount)
            .WithDescription(description)
            .WithOccurredOn(occurredOn)
            .Build();

        // Assert
        movement.UserId.ShouldBe(userId);
        movement.Kind.ShouldBe(kind);
        movement.Amount.ShouldBe(amount);
        movement.Description.ShouldBe(description);
        movement.OccurredOn.ShouldBe(occurredOn);
    }

    [Fact]
    public void Ctor_WhenUserIdIsEmpty_ThenThrowsArgumentException()
    {
        // Act & Assert
        Should.Throw<ArgumentException>(() =>
            new MovementBuilder().WithUserId(Guid.Empty).Build());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Ctor_WhenAmountIsZeroOrNegative_ThenThrowsArgumentOutOfRangeException(decimal invalidAmount)
    {
        // Act & Assert
        Should.Throw<ArgumentOutOfRangeException>(() =>
            new MovementBuilder().WithAmount(invalidAmount).Build());
    }

    [Fact]
    public void ChangeKind_UpdatesKind()
    {
        // Arrange
        var movement = new MovementBuilder().WithKind(MovementKind.Income).Build();

        // Act
        movement.ChangeKind(MovementKind.Expense);

        // Assert
        movement.Kind.ShouldBe(MovementKind.Expense);
    }

    [Fact]
    public void ChangeAmount_WhenValid_ThenUpdatesAmount()
    {
        // Arrange
        var movement = new MovementBuilder().WithAmount(100m).Build();

        // Act
        movement.ChangeAmount(200m);

        // Assert
        movement.Amount.ShouldBe(200m);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ChangeAmount_WhenZeroOrNegative_ThenThrowsArgumentOutOfRangeException(decimal invalidAmount)
    {
        // Arrange
        var movement = new MovementBuilder().Build();

        // Act & Assert
        Should.Throw<ArgumentOutOfRangeException>(() => movement.ChangeAmount(invalidAmount));
    }

    [Fact]
    public void ChangeDescription_UpdatesDescription()
    {
        // Arrange
        var movement = new MovementBuilder().WithDescription("Test movement").Build();

        // Act
        movement.ChangeDescription("Test movement updated");

        // Assert
        movement.Description.ShouldBe("Test movement updated");
    }

    [Fact]
    public void ChangeOccurredOn_UpdatesDate()
    {
        // Arrange
        var movement = new MovementBuilder().WithOccurredOn(DateTimeOffset.UtcNow).Build();
        var newDate = DateTimeOffset.UtcNow.AddDays(-1);

        // Act
        movement.ChangeOccurredOn(newDate);

        // Assert
        movement.OccurredOn.ShouldBe(newDate);
    }
}
