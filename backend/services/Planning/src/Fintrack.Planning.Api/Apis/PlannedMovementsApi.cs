namespace Fintrack.Planning.Api.Apis;

public sealed class PlannedMovementsApi : IApi
{
    public void Map(IEndpointRouteBuilder builder)
    {
        var v1 = builder.MapGroup("planned-movements")
            .RequireAuthorization()
            .WithTags("PlannedMovements")
            .HasApiVersion(1, 0);

        v1.MapGet(string.Empty, () => Results.Ok("OK!"));
    }
}
