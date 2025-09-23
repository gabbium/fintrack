using Fintrack.Ledger.Domain.Entities;
using Fintrack.Ledger.Domain.Enums;

namespace Fintrack.Ledger.Domain.UnitTests.TestHelpers.Builders;

public class MovementBuilder
{
    private Guid _userId = Guid.NewGuid();
    private MovementKind _kind = MovementKind.Income;
    private decimal _amount = 100m;
    private string? _description = "Test movement";
    private DateTimeOffset _occurredOn = DateTimeOffset.UtcNow;

    public MovementBuilder WithUserId(Guid userId)
    {
        _userId = userId;
        return this;
    }

    public MovementBuilder WithKind(MovementKind kind)
    {
        _kind = kind;
        return this;
    }

    public MovementBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public MovementBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public MovementBuilder WithOccurredOn(DateTimeOffset occurredOn)
    {
        _occurredOn = occurredOn;
        return this;
    }

    public Movement Build()
    {
        return new(_userId, _kind, _amount, _description, _occurredOn);
    }
}

