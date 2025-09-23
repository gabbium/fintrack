namespace Fintrack.Ledger.Application.Queries.GetMovementById;

internal sealed class GetMovementByIdValidator
    : AbstractValidator<GetMovementByIdQuery>
{
    public GetMovementByIdValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty();
    }
}
