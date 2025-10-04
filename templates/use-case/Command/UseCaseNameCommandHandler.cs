namespace Fintrack.ServiceName.Application.UseCases.UseCaseName;

internal sealed class UseCaseNameCommandHandler
    : ICommandHandler<UseCaseNameCommand>
{
    public Task<Result> HandleAsync(
        UseCaseNameCommand command,
        CancellationToken cancellationToken = default)
    {
        // TODO: implement command logic
        throw new System.NotImplementedException();
    }
}
