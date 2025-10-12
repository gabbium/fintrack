using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Application.UnitTests.TestSupport.Builders;

namespace Fintrack.Ledger.Application.UnitTests.Models;

public class MovementDtoTests
{
    [Fact]
    public void FromAggregate_MapsAllPropertiesCorrectly()
    {
        // Arrange
        var movement = new MovementBuilder().Build();

        // Act
        var model = MovementDto.FromAggregate(movement);

        // Assert
        model.Id.ShouldBe(movement.Id);
        model.Kind.ShouldBe(movement.Kind);
        model.Amount.ShouldBe(movement.Amount);
        model.Description.ShouldBe(movement.Description);
        model.OccurredOn.ShouldBe(movement.OccurredOn);
    }
}
