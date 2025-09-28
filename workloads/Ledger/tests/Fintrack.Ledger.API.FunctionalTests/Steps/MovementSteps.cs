using Fintrack.Ledger.API.FunctionalTests.TestHelpers;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Builders;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;
using Fintrack.Ledger.Application.Commands.CreateMovement;
using Fintrack.Ledger.Application.Commands.UpdateMovement;
using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Application.Queries.ListMovements;

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

    public UpdateMovementCommand Given_ValidUpdateCommand(Guid id)
    {
        return new UpdateMovementCommandBuilder()
                .WithId(id)
                .Build();
    }

    public UpdateMovementCommand Given_InvalidUpdateCommand_TooLongDescription(Guid id)
    {
        return new UpdateMovementCommandBuilder()
                .WithId(id)
                .WithDescription(new string('a', 129))
                .Build();
    }

    public ListMovementsQuery Given_ValidListQuery()
    {
        return new ListMovementsQueryBuilder()
            .Build();
    }

    public ListMovementsQuery Given_InvalidValidListQuery_PageNumberNegative()
    {
        return new ListMovementsQueryBuilder()
            .WithPageNumber(-1)
            .Build();
    }

    public async Task<MovementDto> Given_ExistingMovement()
    {
        var command = new CreateMovementCommandBuilder().Build();

        var response = await _httpClient.PostAsJsonAsync("/api/v1/movements", command);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<MovementDto>(TestConstants.Json);
        body.ShouldNotBeNull();

        return body;
    }

    public async Task<HttpResponseMessage> When_AttemptToList(ListMovementsQuery query)
    {
        var queryParams = QueryString.Create(new Dictionary<string, string?>
        {
            ["pageNumber"] = query.PageNumber.ToString(),
            ["pageSize"] = query.PageSize.ToString()
        });

        return await _httpClient.GetAsync("/api/v1/movements" + queryParams);
    }

    public async Task<HttpResponseMessage> When_AttemptToGetById(Guid id)
    {
        return await _httpClient.GetAsync("/api/v1/movements/" + id);
    }
    public async Task<HttpResponseMessage> When_AttemptToCreate(CreateMovementCommand command)
    {
        return await _httpClient.PostAsJsonAsync("/api/v1/movements", command);
    }

    public async Task<HttpResponseMessage> When_AttemptToUpdate(Guid id, UpdateMovementCommand command)
    {
        return await _httpClient.PutAsJsonAsync("/api/v1/movements/" + id, command);
    }

    public async Task<HttpResponseMessage> When_AttemptToDelete(Guid id)
    {
        return await _httpClient.DeleteAsync("/api/v1/movements/" + id);
    }
}

