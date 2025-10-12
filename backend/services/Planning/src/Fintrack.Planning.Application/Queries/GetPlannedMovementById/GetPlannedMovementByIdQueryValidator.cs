namespace Fintrack.Planning.Application.Queries.GetPlannedMovementById;

internal sealed class GetPlannedMovementByIdQueryValidator
    : AbstractValidator<GetPlannedMovementByIdQuery>
{
    public GetPlannedMovementByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty();
    }
}
