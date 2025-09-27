using Fintrack.Ledger.Application.MovementAggregate.CreateMovement;

namespace Fintrack.Ledger.API.Apis;

public class MovementsApi : IApi
{
    public void Map(IEndpointRouteBuilder builder)
    {
        var api = builder.MapGroup("movements")
            .HasApiVersion(1.0)
            .WithTags("Movements");

        api.MapPost(string.Empty, CreateMovement)
           .WithName(nameof(CreateMovement));
    }

    public static async Task<IResult> CreateMovement(
        CreateMovementCommand command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync(command, cancellationToken);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : CustomResults.Problem(result);
    }
}
