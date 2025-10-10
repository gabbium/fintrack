using Fintrack.Ledger.Api.Models;
using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Application.UseCases.CreateMovement;
using Fintrack.Ledger.Application.UseCases.DeleteMovement;
using Fintrack.Ledger.Application.UseCases.GetMovementById;
using Fintrack.Ledger.Application.UseCases.ListMovements;
using Fintrack.Ledger.Application.UseCases.UpdateMovement;

namespace Fintrack.Ledger.Api.Apis;

public sealed class MovementsApi : IMinimalApi
{
    public void Map(IEndpointRouteBuilder builder)
    {
        var v1 = builder.MapGroup("movements")
            .WithTags("Movements")
            .RequireAuthorization()
            .HasApiVersion(1, 0);

        v1.MapGet(string.Empty, ListMovements)
            .WithName(nameof(ListMovements))
            .WithSummary("List movements")
            .WithDescription("Get all movements filtered by date range with optional ordering.")
            .Produces<PaginatedList<MovementDto>>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        v1.MapGet("{id:guid}", GetMovementById)
            .WithName(nameof(GetMovementById))
            .WithSummary("Get movement")
            .WithDescription("Retrieve details of a specific movement by its ID.")
            .Produces<MovementDto>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

        v1.MapPost(string.Empty, CreateMovement)
            .WithName(nameof(CreateMovement))
            .WithSummary("Create movement")
            .WithDescription("Record a new expense or income.")
            .Produces<MovementDto>(StatusCodes.Status201Created)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        v1.MapPut("{id:guid}", UpdateMovement)
            .WithName(nameof(UpdateMovement))
            .WithSummary("Update movement")
            .WithDescription("Modify amount, kind, occurrence date, description.")
            .Produces<MovementDto>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

        v1.MapDelete("{id:guid}", DeleteMovement)
            .WithName(nameof(DeleteMovement))
            .WithSummary("Delete movement")
            .WithDescription("Permanently remove a movement from the system.")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);
    }

    public static async Task<IResult> ListMovements(
        [AsParameters] ListMovementsRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new ListMovementsQuery(
            request.PageNumber,
            request.PageSize,
            request.Order,
            request.Kind?.ToList(),
            request.MinOccurredOn,
            request.MaxOccurredOn);

        var result = await mediator.SendAsync(query, cancellationToken);

        return result.ToMinimalApiResult(() => Results.Ok(result.Value));
    }

    public static async Task<IResult> GetMovementById(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetMovementByIdQuery(id);

        var result = await mediator.SendAsync(query, cancellationToken);

        return result.ToMinimalApiResult(() => Results.Ok(result.Value));
    }

    public static async Task<IResult> CreateMovement(
        CreateMovementRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new CreateMovementCommand(
            request.Kind,
            request.Amount,
            request.Description,
            request.OccurredOn);

        var result = await mediator.SendAsync(command, cancellationToken);

        return result.ToMinimalApiResult(() => Results.CreatedAtRoute(
            routeName: nameof(GetMovementById),
            routeValues: new { id = result.Value!.Id },
            value: result.Value));
    }

    public static async Task<IResult> UpdateMovement(
        UpdateMovementRequest request,
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new UpdateMovementCommand(
            id,
            request.Kind,
            request.Amount,
            request.Description,
            request.OccurredOn);

        var result = await mediator.SendAsync(command, cancellationToken);

        return result.ToMinimalApiResult(() => Results.Ok(result.Value));
    }

    public static async Task<IResult> DeleteMovement(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new DeleteMovementCommand(id);

        var result = await mediator.SendAsync(command, cancellationToken);

        return result.ToMinimalApiResult(Results.NoContent);
    }
}
