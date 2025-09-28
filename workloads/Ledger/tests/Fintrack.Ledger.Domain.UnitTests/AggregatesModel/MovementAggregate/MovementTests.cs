using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;
using Fintrack.Ledger.Domain.UnitTests.TestHelpers.Builders;

namespace Fintrack.Ledger.Domain.UnitTests.AggregatesModel.MovementAggregate;

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
}
