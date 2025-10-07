using Fintrack.Planning.Application.UseCases.GetPlannedMovementById;

namespace Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;

public class GetPlannedMovementByIdQueryBuilder
{
    private Guid _id = Guid.NewGuid();

    public GetPlannedMovementByIdQueryBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public GetPlannedMovementByIdQuery Build()
    {
        return new(_id);
    }
}
