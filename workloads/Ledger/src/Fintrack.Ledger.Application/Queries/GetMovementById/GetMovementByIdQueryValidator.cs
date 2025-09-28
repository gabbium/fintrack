namespace Fintrack.Ledger.Application.Queries.GetMovementById;

internal sealed class GetMovementByIdQueryValidator
    : AbstractValidator<GetMovementByIdQuery>
{
    public GetMovementByIdQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty();
    }
}
