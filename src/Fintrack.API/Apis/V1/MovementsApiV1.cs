using Fintrack.Ledger.Application.Commands.CreateMovement;
using Fintrack.Ledger.Application.Commands.DeleteMovement;
using Fintrack.Ledger.Application.Commands.UpdateMovement;
using Fintrack.Ledger.Application.Queries.GetMovementById;
using Fintrack.Ledger.Application.Queries.ListMovements;

namespace Fintrack.API.Apis.V1;

public class MovementsApiV1 : IApi
{
    public void Map(IEndpointRouteBuilder builder)
    {
        var api = builder.MapGroup("movements")
            .HasApiVersion(1.0)
            .WithTags("Movements")
            .RequireAuthorization();

        api.MapGet(string.Empty, ListMovements)
           .WithName(nameof(ListMovements));

        api.MapGet("{id:guid}", GetMovementById)
           .WithName(nameof(GetMovementById));

        api.MapPost(string.Empty, CreateMovement)
           .WithName(nameof(CreateMovement));

        api.MapPut("{id:guid}", UpdateMovement)
           .WithName(nameof(UpdateMovement));

        api.MapDelete("{id:guid}", DeleteMovement)
           .WithName(nameof(DeleteMovement));
    }

    public async Task<IResult> ListMovements(
        [AsParameters] ListMovementsQuery query,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync(query, cancellationToken);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : CustomResults.Problem(result);
    }

    public async Task<IResult> GetMovementById(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetMovementByIdQuery(id);

        var result = await mediator.SendAsync(query, cancellationToken);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : CustomResults.Problem(result);
    }

    public async Task<IResult> CreateMovement(
        CreateMovementCommand command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync(command, cancellationToken);

        return result.IsSuccess
            ? Results.CreatedAtRoute(
                routeName: nameof(GetMovementById),
                routeValues: new { id = result.Value!.Id },
                value: result.Value)
            : CustomResults.Problem(result);
    }

    public async Task<IResult> UpdateMovement(
        UpdateMovementCommand command,
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["id"] = ["Route id must match body id."]
            });
        }

        var result = await mediator.SendAsync(command, cancellationToken);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : CustomResults.Problem(result);
    }

    public async Task<IResult> DeleteMovement(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new DeleteMovementCommand(id);

        var result = await mediator.SendAsync(command, cancellationToken);

        return result.IsSuccess
            ? Results.NoContent()
            : CustomResults.Problem(result);
    }
}

