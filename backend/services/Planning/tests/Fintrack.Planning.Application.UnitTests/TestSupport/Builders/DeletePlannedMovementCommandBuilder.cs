using Fintrack.Planning.Application.Commands.DeletePlannedMovement;

namespace Fintrack.Planning.Application.UnitTests.TestSupport.Builders;

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
