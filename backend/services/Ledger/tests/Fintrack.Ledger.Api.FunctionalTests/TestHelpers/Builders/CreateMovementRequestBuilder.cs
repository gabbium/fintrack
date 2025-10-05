using Fintrack.Ledger.Api.Models;
using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Builders;

public class CreateMovementRequestBuilder
{
    private MovementKind _kind = MovementKind.Income;
    private decimal _amount = 100m;
    private string? _description = "Test movement";
    private DateTimeOffset _occurredOn = DateTimeOffset.UtcNow;

    public CreateMovementRequestBuilder WithKind(MovementKind kind)
    {
        _kind = kind;
        return this;
    }

    public CreateMovementRequestBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public CreateMovementRequestBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public CreateMovementRequestBuilder WithOccurredOn(DateTimeOffset occurredOn)
    {
        _occurredOn = occurredOn;
        return this;
    }

    public CreateMovementRequest Build()
    {
        return new()
        {
            Kind = _kind,
            Amount = _amount,
            Description = _description,
            OccurredOn = _occurredOn
        };
    }
}
