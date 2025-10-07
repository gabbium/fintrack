using Fintrack.Planning.Api.Models;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Api.FunctionalTests.TestHelpers.Builders;

public class UpdatePlannedMovementRequestBuilder
{
    private PlannedMovementKind _kind = PlannedMovementKind.Income;
    private decimal _amount = 100m;
    private string? _description = "Updated planned movement";
    private DateTimeOffset _dueOn = DateTimeOffset.UtcNow.AddDays(3);

    public UpdatePlannedMovementRequestBuilder WithKind(PlannedMovementKind kind)
    {
        _kind = kind;
        return this;
    }

    public UpdatePlannedMovementRequestBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public UpdatePlannedMovementRequestBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public UpdatePlannedMovementRequestBuilder WithDueOn(DateTimeOffset dueOn)
    {
        _dueOn = dueOn;
        return this;
    }

    public UpdatePlannedMovementRequest Build()
    {
        return new()
        {
            Kind = _kind,
            Amount = _amount,
            Description = _description,
            DueOn = _dueOn
        };
    }
}
