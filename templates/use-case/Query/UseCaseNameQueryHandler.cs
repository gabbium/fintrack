namespace Fintrack.ServiceName.Application.UseCases.UseCaseName;

internal sealed class UseCaseNameQueryHandler
    : IQueryHandler<UseCaseNameQuery>
{
    public Task<Result> HandleAsync(
        UseCaseNameQuery query,
        CancellationToken cancellationToken = default)
    {
        // TODO: implement query logic
        throw new System.NotImplementedException();
    }
}
