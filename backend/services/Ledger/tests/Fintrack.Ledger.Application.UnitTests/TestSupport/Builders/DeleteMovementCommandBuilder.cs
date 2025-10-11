using Fintrack.Ledger.Application.UseCases.DeleteMovement;

namespace Fintrack.Ledger.Application.UnitTests.TestSupport.Builders;

public class DeleteMovementCommandBuilder
{
    private Guid _id = Guid.NewGuid();

    public DeleteMovementCommandBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public DeleteMovementCommand Build()
    {
        return new(_id);
    }
}
