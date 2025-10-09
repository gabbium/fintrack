using Fintrack.Planning.Domain.PlannedMovementAggregate;
using Fintrack.Planning.Domain.UnitTests.TestHelpers.Builders;

namespace Fintrack.Planning.Domain.UnitTests.PlannedMovementAggregate;

public class PlannedMovementTests
{
    [Fact]
    public void Ctor_WhenValidArguments_ThenCreatesPlannedMovement()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var kind = PlannedMovementKind.Income;
        var amount = 123.45m;
        var description = "Bonus";
        var dueOn = DateTimeOffset.UtcNow.AddDays(3);
        var status = PlannedMovementStatus.Active;

        // Act
        var movement = new PlannedMovementBuilder()
            .WithUserId(userId)
            .WithKind(kind)
            .WithAmount(amount)
            .WithDescription(description)
            .WithDueOn(dueOn)
            .WithStatus(status)
            .Build();

        // Assert
        movement.UserId.ShouldBe(userId);
        movement.Kind.ShouldBe(kind);
        movement.Amount.ShouldBe(amount);
        movement.Description.ShouldBe(description);
        movement.DueOn.ShouldBe(dueOn);
        movement.Status.ShouldBe(status);
    }

    [Fact]
    public void Ctor_WhenUserIdIsEmpty_ThenThrowsArgumentException()
    {
        // Act & Assert
        Should.Throw<ArgumentException>(() =>
            new PlannedMovementBuilder().WithUserId(Guid.Empty).Build());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Ctor_WhenAmountIsZeroOrNegative_ThenThrowsArgumentOutOfRangeException(decimal invalidAmount)
    {
        // Act & Assert
        Should.Throw<ArgumentOutOfRangeException>(() =>
            new PlannedMovementBuilder().WithAmount(invalidAmount).Build());
    }

    [Fact]
    public void ChangeKind_UpdatesKind()
    {
        // Arrange
        var plannedMovement = new PlannedMovementBuilder().WithKind(PlannedMovementKind.Income).Build();

        // Act
        plannedMovement.ChangeKind(PlannedMovementKind.Expense);

        // Assert
        plannedMovement.Kind.ShouldBe(PlannedMovementKind.Expense);
    }

    [Fact]
    public void ChangeAmount_WhenValid_ThenUpdatesAmount()
    {
        // Arrange
        var plannedMovement = new PlannedMovementBuilder().WithAmount(100m).Build();

        // Act
        plannedMovement.ChangeAmount(200m);

        // Assert
        plannedMovement.Amount.ShouldBe(200m);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ChangeAmount_WhenZeroOrNegative_ThenThrowsArgumentOutOfRangeException(decimal invalidAmount)
    {
        // Arrange
        var plannedMovement = new PlannedMovementBuilder().Build();

        // Act & Assert
        Should.Throw<ArgumentOutOfRangeException>(() => plannedMovement.ChangeAmount(invalidAmount));
    }

    [Fact]
    public void ChangeDescription_UpdatesDescription()
    {
        // Arrange
        var plannedMovement = new PlannedMovementBuilder().WithDescription("Test planned movement").Build();

        // Act
        plannedMovement.ChangeDescription("Test planned movement updated");

        // Assert
        plannedMovement.Description.ShouldBe("Test planned movement updated");
    }

    [Fact]
    public void ChangeDueOn_UpdatesDate()
    {
        // Arrange
        var plannedMovement = new PlannedMovementBuilder().WithDueOn(DateTimeOffset.UtcNow.AddDays(3)).Build();
        var newDate = DateTimeOffset.UtcNow.AddDays(7);

        // Act
        plannedMovement.ChangeDueOn(newDate);

        // Assert
        plannedMovement.DueOn.ShouldBe(newDate);
    }

    [Fact]
    public void Cancel_WhenActive_ThenSetsStatusToCanceled()
    {
        // Arrange
        var plannedMovement = new PlannedMovementBuilder().WithStatus(PlannedMovementStatus.Active).Build();

        // Act
        plannedMovement.Cancel();

        // Assert
        plannedMovement.Status.ShouldBe(PlannedMovementStatus.Canceled);
    }

    [Theory]
    [InlineData(PlannedMovementStatus.Canceled)]
    [InlineData(PlannedMovementStatus.Realized)]
    [InlineData(PlannedMovementStatus.Overdue)]
    public void Cancel_WhenNotActive_ThenThrowsInvalidOperationException(PlannedMovementStatus status)
    {
        // Arrange
        var plannedMovement = new PlannedMovementBuilder().WithStatus(status).Build();

        // Act & Assert
        Should.Throw<InvalidOperationException>(plannedMovement.Cancel);
    }

    [Fact]
    public void Realize_WhenActive_ThenSetsStatusToRealized()
    {
        // Arrange
        var plannedMovement = new PlannedMovementBuilder().WithStatus(PlannedMovementStatus.Active).Build();

        // Act
        plannedMovement.Realize();

        // Assert
        plannedMovement.Status.ShouldBe(PlannedMovementStatus.Realized);
    }

    [Theory]
    [InlineData(PlannedMovementStatus.Canceled)]
    [InlineData(PlannedMovementStatus.Realized)]
    [InlineData(PlannedMovementStatus.Overdue)]
    public void Realize_WhenNotActive_ThenThrowsInvalidOperationException(PlannedMovementStatus status)
    {
        // Arrange
        var plannedMovement = new PlannedMovementBuilder().WithStatus(status).Build();

        // Act & Assert
        Should.Throw<InvalidOperationException>(plannedMovement.Realize);
    }
}
