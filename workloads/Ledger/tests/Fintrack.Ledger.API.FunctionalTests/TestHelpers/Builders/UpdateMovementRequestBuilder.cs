using Fintrack.Ledger.API.Apis;
using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Builders;

public class UpdateMovementRequestBuilder
{
    private MovementKind _kind = MovementKind.Income;
    private decimal _amount = 100m;
    private string? _description = "Updated movement";
    private DateTimeOffset _occurredOn = DateTimeOffset.UtcNow;

    public UpdateMovementRequestBuilder WithKind(MovementKind kind)
    {
        _kind = kind;
        return this;
    }

    public UpdateMovementRequestBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public UpdateMovementRequestBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public UpdateMovementRequestBuilder WithOccurredOn(DateTimeOffset occurredOn)
    {
        _occurredOn = occurredOn;
        return this;
    }

    public UpdateMovementRequest Build()
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
