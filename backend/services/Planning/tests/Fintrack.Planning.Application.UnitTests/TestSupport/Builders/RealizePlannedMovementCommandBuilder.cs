using Fintrack.Planning.Application.Commands.RealizePlannedMovement;

namespace Fintrack.Planning.Application.UnitTests.TestSupport.Builders;

public class RealizePlannedMovementCommandBuilder
{
    private Guid _id = Guid.NewGuid();

    public RealizePlannedMovementCommandBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public RealizePlannedMovementCommand Build()
    {
        return new(_id);
    }
}
