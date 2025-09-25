namespace Fintrack.Ledger.API.Apis;

public class HelloApi : IApi
{
    public void Map(IEndpointRouteBuilder builder)
    {
        var api = builder.MapGroup("hello")
            .HasApiVersion(1.0)
            .WithTags("Hello");

        api.MapGet(string.Empty, () => Results.Ok("Hello world!"));
    }
}
