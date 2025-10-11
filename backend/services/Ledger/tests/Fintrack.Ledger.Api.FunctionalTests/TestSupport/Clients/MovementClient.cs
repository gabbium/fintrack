using Fintrack.Ledger.Api.FunctionalTests.TestSupport.Builders;
using Fintrack.Ledger.Api.Models;
using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.Api.FunctionalTests.TestSupport.Clients;

public class MovementClient(HttpClient httpClient)
{
    public async Task<HttpResponseMessage> ListMovements(ListMovementsRequest request)
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

        return await httpClient.GetAsync("/api/v1/movements" + queryString);
    }

    public async Task<HttpResponseMessage> GetMovementById(Guid id)
    {
        return await httpClient.GetAsync("/api/v1/movements/" + id);
    }

    public async Task<HttpResponseMessage> CreateMovement(CreateMovementRequest request)
    {
        return await httpClient.PostAsJsonAsync("/api/v1/movements", request);
    }

    public async Task<HttpResponseMessage> UpdateMovement(Guid id, UpdateMovementRequest request)
    {
        return await httpClient.PutAsJsonAsync("/api/v1/movements/" + id, request);
    }

    public async Task<HttpResponseMessage> DeleteMovement(Guid id)
    {
        return await httpClient.DeleteAsync("/api/v1/movements/" + id);
    }

    public async Task<MovementDto> EnsureMovementExists(CreateMovementRequest? request = null)
    {
        request ??= new CreateMovementRequestBuilder().Build();
        var response = await CreateMovement(request);
        return await response.ShouldBeCreatedWithBody<MovementDto>();
    }
}
