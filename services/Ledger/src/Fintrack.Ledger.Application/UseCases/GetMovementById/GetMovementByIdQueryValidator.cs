namespace Fintrack.Ledger.Application.UseCases.GetMovementById;

internal sealed class GetMovementByIdQueryValidator
    : AbstractValidator<GetMovementByIdQuery>
{
    public GetMovementByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty();
    }
}
