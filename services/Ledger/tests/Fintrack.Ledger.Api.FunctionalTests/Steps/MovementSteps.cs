using Fintrack.Ledger.Api.FunctionalTests.TestHelpers;
using Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Builders;
using Fintrack.Ledger.Api.Models;
using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.Api.FunctionalTests.Steps;

public class MovementSteps(TestFixture fx)
{
    private readonly HttpClient _httpClient = fx.Factory.CreateDefaultClient();

    public async Task<MovementDto> Given_ExistingMovement(CreateMovementRequest? request = null)
    {
        request ??= new CreateMovementRequestBuilder().Build();

        var response = await _httpClient.PostAsJsonAsync("/api/v1/movements", request);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<MovementDto>(TestConstants.Json);
        body.ShouldNotBeNull();

        return body;
    }

    public async Task<HttpResponseMessage> When_AttemptToList(ListMovementsRequest request)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["pageNumber"] = request.PageNumber.ToString(),
            ["pageSize"] = request.PageSize.ToString()
        };

        if (request.Order is not null)
        {
            queryParams["order"] = request.Order;
        }

        if (request.Kind is { Length: > 0 })
        {
            foreach (var kind in request.Kind)
            {
                queryParams.Add("kind", kind.ToString());
            }
        }

        if (request.MinOccurredOn is not null)
        {
            queryParams.Add("minOccurredOn", request.MinOccurredOn.Value.ToString("O"));
        }

        if (request.MaxOccurredOn is not null)
        {
            queryParams.Add("maxOccurredOn", request.MaxOccurredOn.Value.ToString("O"));
        }

        var queryString = QueryString.Create(queryParams);

        return await _httpClient.GetAsync("/api/v1/movements" + queryString);
    }

    public async Task<HttpResponseMessage> When_AttemptToGetById(Guid id)
    {
        return await _httpClient.GetAsync("/api/v1/movements/" + id);
    }
    public async Task<HttpResponseMessage> When_AttemptToCreate(CreateMovementRequest request)
    {
        return await _httpClient.PostAsJsonAsync("/api/v1/movements", request);
    }

    public async Task<HttpResponseMessage> When_AttemptToUpdate(Guid id, UpdateMovementRequest request)
    {
        return await _httpClient.PutAsJsonAsync("/api/v1/movements/" + id, request);
    }

    public async Task<HttpResponseMessage> When_AttemptToDelete(Guid id)
    {
        return await _httpClient.DeleteAsync("/api/v1/movements/" + id);
    }
}
