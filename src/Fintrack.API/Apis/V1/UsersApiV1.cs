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
        api.MapGet("/me", Me).RequireAuthorization();
    }

    public async Task<IResult> PasswordlessLogin(
        PasswordlessLoginCommand command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendAsync(command, cancellationToken);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : CustomResults.Problem(result);
    }

    public IResult Me(HttpContext context)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = context.User.FindFirst(ClaimTypes.Email)?.Value;
        return Results.Ok(new { userId, email });
    }
}

