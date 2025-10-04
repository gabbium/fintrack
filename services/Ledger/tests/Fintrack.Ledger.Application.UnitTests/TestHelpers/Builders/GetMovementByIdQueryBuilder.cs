using Fintrack.Ledger.Application.UseCases.GetMovementById;

namespace Fintrack.Ledger.Application.UnitTests.TestHelpers.Builders;

public class GetMovementByIdQueryBuilder
{
    private Guid _id = Guid.NewGuid();

    public GetMovementByIdQueryBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public GetMovementByIdQuery Build()
    {
        return new(_id);
    }
}
