namespace Fintrack.Ledger.Application.Queries.ListMovements;

internal sealed class ListMovementsQueryValidator
    : AbstractValidator<ListMovementsQuery>
{
    public ListMovementsQueryValidator()
    {
        RuleFor(query => query.PageNumber)
            .GreaterThan(0);

        RuleFor(query => query.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
    }
}
