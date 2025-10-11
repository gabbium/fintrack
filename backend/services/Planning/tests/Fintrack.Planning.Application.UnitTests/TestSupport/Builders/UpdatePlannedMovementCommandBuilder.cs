using Fintrack.Planning.Application.UseCases.UpdatePlannedMovement;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;

public class UpdatePlannedMovementCommandBuilder
{
    private Guid _id = Guid.NewGuid();
    private PlannedMovementKind _kind = PlannedMovementKind.Income;
    private decimal _amount = 100m;
    private string? _description = "Updated planned movement";
    private DateTimeOffset _dueOn = DateTimeOffset.UtcNow.AddDays(3);

    public UpdatePlannedMovementCommandBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public UpdatePlannedMovementCommandBuilder WithKind(PlannedMovementKind kind)
    {
        _kind = kind;
        return this;
    }

    public UpdatePlannedMovementCommandBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public UpdatePlannedMovementCommandBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public UpdatePlannedMovementCommandBuilder WithDueOn(DateTimeOffset dueOn)
    {
        _dueOn = dueOn;
        return this;
    }

    public UpdatePlannedMovementCommand Build()
    {
        return new(_id, _kind, _amount, _description, _dueOn);
    }
}
