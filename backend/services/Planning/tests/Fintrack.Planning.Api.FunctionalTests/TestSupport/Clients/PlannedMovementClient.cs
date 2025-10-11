using Fintrack.Planning.Api.FunctionalTests.TestSupport.Builders;
using Fintrack.Planning.Api.Models;
using Fintrack.Planning.Application.Models;

namespace Fintrack.Planning.Api.FunctionalTests.TestSupport.Clients;

public class PlannedMovementClient(HttpClient httpClient)
{
    public async Task<HttpResponseMessage> ListPlannedMovements(ListPlannedMovementsRequest request)
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

        return await httpClient.GetAsync("/api/v1/planned-movements" + queryString);
    }

    public async Task<HttpResponseMessage> GetPlannedMovementById(Guid id)
    {
        return await httpClient.GetAsync("/api/v1/planned-movements/" + id);
    }

    public async Task<HttpResponseMessage> CreatePlannedMovement(CreatePlannedMovementRequest request)
    {
        return await httpClient.PostAsJsonAsync("/api/v1/planned-movements", request);
    }

    public async Task<HttpResponseMessage> UpdatePlannedMovement(Guid id, UpdatePlannedMovementRequest request)
    {
        return await httpClient.PutAsJsonAsync("/api/v1/planned-movements/" + id, request);
    }

    public async Task<HttpResponseMessage> RealizePlannedMovement(Guid id)
    {
        return await httpClient.PostAsync("/api/v1/planned-movements/" + id + "/realize", null);
    }

    public async Task<HttpResponseMessage> CancelPlannedMovement(Guid id)
    {
        return await httpClient.PostAsync("/api/v1/planned-movements/" + id + "/cancel", null);
    }

    public async Task<HttpResponseMessage> DeletePlannedMovement(Guid id)
    {
        return await httpClient.DeleteAsync("/api/v1/planned-movements/" + id);
    }

    public async Task<PlannedMovementDto> EnsureActivePlannedMovementExists(CreatePlannedMovementRequest? request = null)
    {
        request ??= new CreatePlannedMovementRequestBuilder().Build();
        var response = await CreatePlannedMovement(request);
        return await response.ShouldBeCreatedWithBody<PlannedMovementDto>();
    }

    public async Task<PlannedMovementDto> EnsureRealizedPlannedMovementExists(CreatePlannedMovementRequest? request = null)
    {
        var body = await EnsureActivePlannedMovementExists(request);
        var response = await RealizePlannedMovement(body.Id);
        response.ShouldBeNoContent();
        return body;
    }

    public async Task<PlannedMovementDto> EnsureCanceledPlannedMovementExists(CreatePlannedMovementRequest? request = null)
    {
        var body = await EnsureActivePlannedMovementExists(request);
        var response = await CancelPlannedMovement(body.Id);
        response.ShouldBeNoContent();
        return body;
    }
}
