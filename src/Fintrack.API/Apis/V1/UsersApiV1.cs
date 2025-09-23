using Fintrack.Identity.Application.Commands.PasswordlessLogin;

namespace Fintrack.API.Apis.V1;

public class UsersApiV1 : IApi
{
    public void Map(IEndpointRouteBuilder builder)
    {
        var api = builder.MapGroup("users")
            .HasApiVersion(1.0)
            .WithTags("Users");

        api.MapPost("/passwordless-login", PasswordlessLogin).AllowAnonymous();
    }

    public static async Task<IResult> PasswordlessLogin(
        PasswordlessLoginCommand command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync(command, cancellationToken);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : CustomResults.Problem(result);
    }
}

