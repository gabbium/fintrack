using Fintrack.Ledger.Application.UseCases.CreateMovement;
using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Application.UnitTests.TestHelpers.Builders;

public class CreateMovementCommandBuilder
{
    private MovementKind _kind = MovementKind.Income;
    private decimal _amount = 100m;
    private string? _description = "Test movement";
    private DateTimeOffset _occurredOn = DateTimeOffset.UtcNow;

    public CreateMovementCommandBuilder WithKind(MovementKind kind)
    {
        _kind = kind;
        return this;
    }

    public CreateMovementCommandBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public CreateMovementCommandBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public CreateMovementCommandBuilder WithOccurredOn(DateTimeOffset occurredOn)
    {
        _occurredOn = occurredOn;
        return this;
    }

    public CreateMovementCommand Build()
    {
        return new(_kind, _amount, _description, _occurredOn);
    }
}
