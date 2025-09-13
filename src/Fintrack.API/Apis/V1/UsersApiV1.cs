using Fintrack.Identity.Application.Commands.CreateUser;

namespace Fintrack.API.Apis.V1;

public class UsersApiV1 : IApi
{
    public void Map(IEndpointRouteBuilder builder)
    {
        var api = builder.MapGroup("users")
            .HasApiVersion(1.0)
            .WithTags("Users");

        api.MapPost("/", CreateMovementAsync);
    }

    public async Task<IResult> CreateMovementAsync(
        CreateUserCommand command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync(command, cancellationToken);

        return result.IsSuccess
            ? Results.NoContent()
            : CustomResults.Problem(result);
    }
}
