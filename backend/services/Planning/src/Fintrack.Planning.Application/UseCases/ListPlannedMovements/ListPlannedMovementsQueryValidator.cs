namespace Fintrack.Planning.Application.UseCases.ListPlannedMovements;

internal sealed class ListPlannedMovementsQueryValidator
    : AbstractValidator<ListPlannedMovementsQuery>
{
    public ListPlannedMovementsQueryValidator()
    {
        RuleFor(query => query.PageNumber)
            .GreaterThan(0);

        RuleFor(query => query.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(query => query.Order)
            .Matches("^(dueon|amount) (asc|desc)$", RegexOptions.IgnoreCase)
            .When(query => query.Order is not null);

        RuleFor(query => query.MinDueOn)
            .LessThanOrEqualTo(query => query.MaxDueOn)
            .When(query => query.MinDueOn is not null && query.MaxDueOn is not null);
    }
}
