using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Builders;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;
using Fintrack.Ledger.Application.Commands.CreateMovement;

namespace Fintrack.Ledger.API.FunctionalTests.Steps;

public class MovementSteps(TestFixture fx)
{
    private readonly HttpClient _httpClient = fx.Factory.CreateDefaultClient();

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

    public async Task<HttpResponseMessage> When_AttemptToCreate(CreateMovementCommand command)
    {
        return await _httpClient.PostAsJsonAsync("/api/v1/movements", command);
    }
}

