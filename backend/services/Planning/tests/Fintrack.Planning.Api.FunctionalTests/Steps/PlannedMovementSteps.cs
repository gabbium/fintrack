using BuildingBlocks.Api.FunctionalTests.Assertions;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers.Builders;
using Fintrack.Planning.Api.Models;
using Fintrack.Planning.Application.Models;

namespace Fintrack.Planning.Api.FunctionalTests.Steps;

public class PlannedMovementSteps(FunctionalTestsFixture fx)
{
    private readonly HttpClient _httpClient = fx.Factory.CreateDefaultClient();

    public async Task<PlannedMovementDto> Given_ExistingPlannedMovement(CreatePlannedMovementRequest? request = null)
    {
        request ??= new CreatePlannedMovementRequestBuilder().Build();

        var response = await _httpClient.PostAsJsonAsync("/api/v1/planned-movements", request);
        return await response.ShouldBeCreatedWithBody<PlannedMovementDto>();
    }

    public async Task<PlannedMovementDto> Given_ExistingRealizedPlannedMovement(CreatePlannedMovementRequest? request = null)
    {
        var body = await Given_ExistingPlannedMovement(request);

        var response = await _httpClient.PostAsync("/api/v1/planned-movements/" + body.Id + "/realize", null);
        response.ShouldBeNoContent();

        return body;
    }

    public async Task<PlannedMovementDto> Given_ExistingCanceledPlannedMovement(CreatePlannedMovementRequest? request = null)
    {
        var body = await Given_ExistingPlannedMovement(request);

        var response = await _httpClient.PostAsync("/api/v1/planned-movements/" + body.Id + "/cancel", null);
        response.ShouldBeNoContent();

        return body;
    }

    public async Task<HttpResponseMessage> When_AttemptToList(ListPlannedMovementsRequest request)
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

        if (request.Status is { Length: > 0 })
        {
            foreach (var status in request.Status)
            {
                queryParams.Add("status", status.ToString());
            }
        }

        if (request.MinDueOn is not null)
        {
            queryParams.Add("minDueOn", request.MinDueOn.Value.ToString("O"));
        }

        if (request.MaxDueOn is not null)
        {
            queryParams.Add("maxDueOn", request.MaxDueOn.Value.ToString("O"));
        }

        var queryString = QueryString.Create(queryParams);

        return await _httpClient.GetAsync("/api/v1/planned-movements" + queryString);
    }

    public async Task<HttpResponseMessage> When_AttemptToGetById(Guid id)
    {
        return await _httpClient.GetAsync("/api/v1/planned-movements/" + id);
    }

    public async Task<HttpResponseMessage> When_AttemptToCreate(CreatePlannedMovementRequest request)
    {
        return await _httpClient.PostAsJsonAsync("/api/v1/planned-movements", request);
    }

    public async Task<HttpResponseMessage> When_AttemptToUpdate(Guid id, UpdatePlannedMovementRequest request)
    {
        return await _httpClient.PutAsJsonAsync("/api/v1/planned-movements/" + id, request);
    }

    public async Task<HttpResponseMessage> When_AttemptToRealize(Guid id)
    {
        return await _httpClient.PostAsync("/api/v1/planned-movements/" + id + "/realize", null);
    }

    public async Task<HttpResponseMessage> When_AttemptToCancel(Guid id)
    {
        return await _httpClient.PostAsync("/api/v1/planned-movements/" + id + "/cancel", null);
    }

    public async Task<HttpResponseMessage> When_AttemptToDelete(Guid id)
    {
        return await _httpClient.DeleteAsync("/api/v1/planned-movements/" + id);
    }
}
