using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;

namespace Fintrack.Planning.Application.UnitTests.Models;

public class PlannedMovementDtoTests
{
    [Fact]
    public void FromDomain_MapsAllPropertiesCorrectly()
    {
        // Arrange
        var plannedMovement = new PlannedMovementBuilder().Build();

        // Act
        var model = PlannedMovementDto.FromDomain(plannedMovement);

        // Assert
        model.Id.ShouldBe(plannedMovement.Id);
        model.Kind.ShouldBe(plannedMovement.Kind);
        model.Amount.ShouldBe(plannedMovement.Amount);
        model.Description.ShouldBe(plannedMovement.Description);
        model.DueOn.ShouldBe(plannedMovement.DueOn);
        model.Status.ShouldBe(plannedMovement.Status);
    }
}
