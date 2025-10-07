namespace Fintrack.Planning.Application.UseCases.GetPlannedMovementById;

internal sealed class GetPlannedMovementByIdQueryValidator
    : AbstractValidator<GetPlannedMovementByIdQuery>
{
    public GetPlannedMovementByIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty();
    }
}
