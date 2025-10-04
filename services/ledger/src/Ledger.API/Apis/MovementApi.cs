using CleanArch.AspNetCore;

namespace Ledger.API.Apis;

public sealed class MovementApi : IApi
{
    public void Map(IEndpointRouteBuilder builder)
    {
        var v1 = builder.MapGroup("movements")
            .WithTags("Movements")
            .HasApiVersion(1, 0);

        v1.MapGet("/", () => "OK V1");
    }
}
