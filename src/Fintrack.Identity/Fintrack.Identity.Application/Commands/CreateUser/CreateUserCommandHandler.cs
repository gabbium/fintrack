namespace Fintrack.Identity.Application.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    public Task<Result> HandleAsync(CreateUserCommand request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Result.Success());
    }
}
