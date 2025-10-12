using Fintrack.Planning.Application.Commands.CreatePlannedMovement;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UnitTests.TestSupport.Builders;

public class CreatePlannedMovementCommandBuilder
{
    private PlannedMovementKind _kind = PlannedMovementKind.Income;
    private decimal _amount = 100m;
    private string? _description = "Test planned movement";
    private DateTimeOffset _dueOn = DateTimeOffset.UtcNow.AddDays(3);

    public CreatePlannedMovementCommandBuilder WithKind(PlannedMovementKind kind)
    {
        _kind = kind;
        return this;
    }

    public CreatePlannedMovementCommandBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public CreatePlannedMovementCommandBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public CreatePlannedMovementCommandBuilder WithDueOn(DateTimeOffset dueOn)
    {
        _dueOn = dueOn;
        return this;
    }

    public CreatePlannedMovementCommand Build()
    {
        return new(_kind, _amount, _description, _dueOn);
    }
}
