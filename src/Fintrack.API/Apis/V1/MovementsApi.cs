namespace Fintrack.API.Apis.V1;

public class MovementsApiV1 : IApi
{
    public void Map(IEndpointRouteBuilder builder)
    {
        var api = builder.MapGroup("movements")
            .HasApiVersion(1.0);

        api.MapPost("/", () => Results.Ok("movements.v1"));
    }
}
