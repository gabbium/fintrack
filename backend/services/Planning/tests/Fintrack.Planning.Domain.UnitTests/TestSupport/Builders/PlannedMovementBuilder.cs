using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Domain.UnitTests.TestSupport.Builders;

public class PlannedMovementBuilder
{
    private Guid _userId = Guid.NewGuid();
    private PlannedMovementKind _kind = PlannedMovementKind.Income;
    private decimal _amount = 100m;
    private string? _description = "Test planned movement";
    private DateTimeOffset _dueOn = DateTimeOffset.UtcNow.AddDays(3);
    private PlannedMovementStatus _status = PlannedMovementStatus.Active;

    public PlannedMovementBuilder WithUserId(Guid userId)
    {
        _userId = userId;
        return this;
    }

    public PlannedMovementBuilder WithKind(PlannedMovementKind kind)
    {
        _kind = kind;
        return this;
    }

    public PlannedMovementBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public PlannedMovementBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public PlannedMovementBuilder WithDueOn(DateTimeOffset dueOn)
    {
        _dueOn = dueOn;
        return this;
    }

    public PlannedMovementBuilder WithStatus(PlannedMovementStatus status)
    {
        _status = status;
        return this;
    }

    public PlannedMovement Build()
    {
        return new(_userId, _kind, _amount, _description, _dueOn, _status);
    }
}
