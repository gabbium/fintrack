using Fintrack.Planning.Api.Models;
using Fintrack.Planning.Application.Commands.CancelPlannedMovement;
using Fintrack.Planning.Application.Commands.CreatePlannedMovement;
using Fintrack.Planning.Application.Commands.DeletePlannedMovement;
using Fintrack.Planning.Application.Commands.RealizePlannedMovement;
using Fintrack.Planning.Application.Commands.UpdatePlannedMovement;
using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Application.Queries.GetPlannedMovementById;
using Fintrack.Planning.Application.Queries.ListPlannedMovements;

namespace Fintrack.Planning.Api.Apis;

public sealed class PlannedMovementsApi : IMinimalApi
{
    public void Map(IEndpointRouteBuilder app)
    {
        var v1 = app.MapGroup("planned-movements")
            .WithTags("Planned Movements")
            .RequireAuthorization()
            .HasApiVersion(1, 0);

        v1.MapGet(string.Empty, ListPlannedMovements)
            .WithName(nameof(ListPlannedMovements))
            .WithSummary("List planned movements")
            .WithDescription("Get all planned movements filtered by status and date range.")
            .Produces<PaginatedList<PlannedMovementDto>>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        v1.MapGet("{id:guid}", GetPlannedMovementById)
            .WithName(nameof(GetPlannedMovementById))
            .WithSummary("Get planned movement")
            .WithDescription("Retrieve details of a specific planned movement by its ID.")
            .Produces<PlannedMovementDto>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

        v1.MapPost(string.Empty, CreatePlannedMovement)
            .WithName(nameof(CreatePlannedMovement))
            .WithSummary("Create planned movement")
            .WithDescription("Register a new planned expense or income with an expected due date.")
            .Produces<PlannedMovementDto>(StatusCodes.Status201Created)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        v1.MapPut("{id:guid}", UpdatePlannedMovement)
            .WithName(nameof(UpdatePlannedMovement))
            .WithSummary("Update planned movement")
            .WithDescription("Modify amount, kind, date, or description of an existing planned movement.")
            .Produces<PlannedMovementDto>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        v1.MapPost("{id:guid}/realize", RealizePlannedMovement)
            .WithName(nameof(RealizePlannedMovement))
            .WithSummary("Realize planned movement")
            .WithDescription("Mark a planned movement as realized.")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        v1.MapPost("{id:guid}/cancel", CancelPlannedMovement)
            .WithName(nameof(CancelPlannedMovement))
            .WithSummary("Cancel planned movement")
            .WithDescription("Cancel a planned movement and set its status to canceled.")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        v1.MapDelete("{id:guid}", DeletePlannedMovement)
            .WithName(nameof(DeletePlannedMovement))
            .WithSummary("Delete planned movement")
            .WithDescription("Permanently remove a planned movement from the system.")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);
    }

    public static async Task<IResult> ListPlannedMovements(
        [AsParameters] ListPlannedMovementsRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new ListPlannedMovementsQuery(
            request.PageNumber,
            request.PageSize,
            request.Order,
            request.Kind?.ToList(),
            request.Status?.ToList(),
            request.MinDueOn,
            request.MaxDueOn);

        var result = await mediator.SendAsync(query, cancellationToken);

        return result.ToMinimalApiResult(() => Results.Ok(result.Value));
    }

    public static async Task<IResult> GetPlannedMovementById(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetPlannedMovementByIdQuery(id);

        var result = await mediator.SendAsync(query, cancellationToken);

        return result.ToMinimalApiResult(() => Results.Ok(result.Value));
    }

    public static async Task<IResult> CreatePlannedMovement(
        CreatePlannedMovementRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new CreatePlannedMovementCommand(
            request.Kind,
            request.Amount,
            request.Description,
            request.DueOn);

        var result = await mediator.SendAsync(command, cancellationToken);

        return result.ToMinimalApiResult(() => Results.CreatedAtRoute(
            routeName: nameof(GetPlannedMovementById),
            routeValues: new { id = result.Value!.Id },
            value: result.Value));
    }

    public static async Task<IResult> UpdatePlannedMovement(
        UpdatePlannedMovementRequest request,
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new UpdatePlannedMovementCommand(
            id,
            request.Kind,
            request.Amount,
            request.Description,
            request.DueOn);

        var result = await mediator.SendAsync(command, cancellationToken);

        return result.ToMinimalApiResult(() => Results.Ok(result.Value));
    }

    public static async Task<IResult> RealizePlannedMovement(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new RealizePlannedMovementCommand(id);

        var result = await mediator.SendAsync(command, cancellationToken);

        return result.ToMinimalApiResult(Results.NoContent);
    }

    public static async Task<IResult> CancelPlannedMovement(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new CancelPlannedMovementCommand(id);

        var result = await mediator.SendAsync(command, cancellationToken);

        return result.ToMinimalApiResult(Results.NoContent);
    }

    public static async Task<IResult> DeletePlannedMovement(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new DeletePlannedMovementCommand(id);

        var result = await mediator.SendAsync(command, cancellationToken);

        return result.ToMinimalApiResult(Results.NoContent);
    }
}
