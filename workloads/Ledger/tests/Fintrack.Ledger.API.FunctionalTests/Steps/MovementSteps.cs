using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Builders;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure;
using Fintrack.Ledger.Application.UseCases.Movements.CreateMovement;

namespace Fintrack.Ledger.API.FunctionalTests.Steps;

public class MovementSteps(TestFixture fx)
{
    public CreateMovementCommand Given_ValidCreateCommand()
    {
        return new CreateMovementCommandBuilder().Build();
    }

    public CreateMovementCommand Given_InvalidCreateCommand_TooLongDescription()
    {
        return new CreateMovementCommandBuilder()
                .WithDescription(new string('a', 129))
                .Build();
    }

    public async Task<HttpResponseMessage> When_AttemptToGetById(Guid id)
    {
        return await fx.Client.GetAsync("/api/v1/movements/" + id);
    }

    public async Task<HttpResponseMessage> When_AttemptToCreate(CreateMovementCommand command)
    {
        return await fx.Client.PostAsJsonAsync("/api/v1/movements", command);
    }

    public async Task<HttpResponseMessage> When_AttemptToDelete(Guid id)
    {
        return await fx.Client.DeleteAsync("/api/v1/movements/" + id);
    }
}
