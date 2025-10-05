namespace Fintrack.Ledger.Application.UseCases.ListMovements;

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

        RuleFor(query => query.Order)
            .Matches("^(occurredon|amount) (asc|desc)$", RegexOptions.IgnoreCase)
            .When(query => query.Order is not null);

        RuleFor(query => query.MinOccurredOn)
            .LessThanOrEqualTo(query => query.MaxOccurredOn)
            .When(query => query.MinOccurredOn is not null && query.MaxOccurredOn is not null);
    }
}
