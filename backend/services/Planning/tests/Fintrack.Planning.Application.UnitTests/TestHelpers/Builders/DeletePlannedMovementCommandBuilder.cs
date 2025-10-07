using Fintrack.Planning.Application.UseCases.DeletePlannedMovement;

namespace Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;

public class DeletePlannedMovementCommandBuilder
{
    private Guid _id = Guid.NewGuid();

    public DeletePlannedMovementCommandBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public DeletePlannedMovementCommand Build()
    {
        return new(_id);
    }
}
