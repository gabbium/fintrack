using Fintrack.Planning.Api.Models;
using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Application.UseCases.CancelPlannedMovement;
using Fintrack.Planning.Application.UseCases.CreatePlannedMovement;
using Fintrack.Planning.Application.UseCases.DeletePlannedMovement;
using Fintrack.Planning.Application.UseCases.GetPlannedMovementById;
using Fintrack.Planning.Application.UseCases.ListPlannedMovements;
using Fintrack.Planning.Application.UseCases.RealizePlannedMovement;
using Fintrack.Planning.Application.UseCases.UpdatePlannedMovement;

namespace Fintrack.Planning.Api.Apis;

public sealed class PlannedMovementsApi : IApi
{
    public void Map(IEndpointRouteBuilder builder)
    {
        var v1 = builder.MapGroup("planned-movements")
            .RequireAuthorization()
            .WithTags("PlannedMovements")
            .HasApiVersion(1, 0);

        v1.MapGet(string.Empty, ListPlannedMovements)
            .WithName(nameof(ListPlannedMovements))
            .Produces<PaginatedList<PlannedMovementDto>>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        v1.MapGet("{id:guid}", GetPlannedMovementById)
            .WithName(nameof(GetPlannedMovementById))
            .Produces<PlannedMovementDto>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

        v1.MapPost(string.Empty, CreatePlannedMovement)
            .WithName(nameof(CreatePlannedMovement))
            .Produces<PlannedMovementDto>(StatusCodes.Status201Created)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        v1.MapPut("{id:guid}", UpdatePlannedMovement)
            .WithName(nameof(UpdatePlannedMovement))
            .Produces<PlannedMovementDto>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        v1.MapPost("{id:guid}/realize", RealizePlannedMovement)
            .WithName(nameof(RealizePlannedMovement))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        v1.MapPost("{id:guid}/cancel", CancelPlannedMovement)
            .WithName(nameof(CancelPlannedMovement))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        v1.MapDelete("{id:guid}", DeletePlannedMovement)
            .WithName(nameof(DeletePlannedMovement))
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

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : CustomResults.Problem(result);
    }

    public static async Task<IResult> GetPlannedMovementById(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new GetPlannedMovementByIdQuery(id);

        var result = await mediator.SendAsync(query, cancellationToken);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : CustomResults.Problem(result);
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

        return result.IsSuccess
            ? Results.CreatedAtRoute(
                routeName: nameof(GetPlannedMovementById),
                routeValues: new { id = result.Value!.Id },
                value: result.Value)
            : CustomResults.Problem(result);
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

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : CustomResults.Problem(result);
    }

    public static async Task<IResult> RealizePlannedMovement(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new RealizePlannedMovementCommand(id);

        var result = await mediator.SendAsync(command, cancellationToken);

        return result.IsSuccess
            ? Results.NoContent()
            : CustomResults.Problem(result);
    }

    public static async Task<IResult> CancelPlannedMovement(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new CancelPlannedMovementCommand(id);

        var result = await mediator.SendAsync(command, cancellationToken);

        return result.IsSuccess
            ? Results.NoContent()
            : CustomResults.Problem(result);
    }

    public static async Task<IResult> DeletePlannedMovement(
        Guid id,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new DeletePlannedMovementCommand(id);

        var result = await mediator.SendAsync(command, cancellationToken);

        return result.IsSuccess
            ? Results.NoContent()
            : CustomResults.Problem(result);
    }
}
