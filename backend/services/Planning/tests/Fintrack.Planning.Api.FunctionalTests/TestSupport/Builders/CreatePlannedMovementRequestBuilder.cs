using Fintrack.Planning.Api.Models;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Api.FunctionalTests.TestSupport.Builders;

public class CreatePlannedMovementRequestBuilder
{
    private PlannedMovementKind _kind = PlannedMovementKind.Income;
    private decimal _amount = 100m;
    private string? _description = "Test planned movement";
    private DateTimeOffset _dueOn = DateTimeOffset.UtcNow.AddDays(3);

    public CreatePlannedMovementRequestBuilder WithKind(PlannedMovementKind kind)
    {
        _kind = kind;
        return this;
    }

    public CreatePlannedMovementRequestBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public CreatePlannedMovementRequestBuilder WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public CreatePlannedMovementRequestBuilder WithDueOn(DateTimeOffset dueOn)
    {
        _dueOn = dueOn;
        return this;
    }

    public CreatePlannedMovementRequest Build()
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
