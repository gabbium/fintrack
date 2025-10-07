using Fintrack.Planning.Application.UseCases.CancelPlannedMovement;

namespace Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;

public class CancelPlannedMovementCommandBuilder
{
    private Guid _id = Guid.NewGuid();

    public CancelPlannedMovementCommandBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public CancelPlannedMovementCommand Build()
    {
        return new(_id);
    }
}
