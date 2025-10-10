namespace Fintrack.Planning.Domain.PlannedMovementAggregate;

public sealed class PlannedMovement : HasDomainEventsBase, IAggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public PlannedMovementKind Kind { get; private set; }
    public decimal Amount { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset DueOn { get; private set; }
    public PlannedMovementStatus Status { get; private set; }

    public PlannedMovement(
        Guid userId,
        PlannedMovementKind kind,
        decimal amount,
        string? description,
        DateTimeOffset dueOn,
        PlannedMovementStatus status)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("User id cannot be empty.", nameof(userId));
        }

        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }

        Id = Guid.NewGuid();
        UserId = userId;
        Kind = kind;
        Amount = amount;
        Description = description;
        DueOn = dueOn;
        Status = status;
    }

    public void ChangeKind(PlannedMovementKind newKind)
    {
        Kind = newKind;
    }

    public void ChangeAmount(decimal newAmount)
    {
        if (newAmount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(newAmount), "Amount must be greater than zero.");
        }

        Amount = newAmount;
    }

    public void ChangeDescription(string? newDescription)
    {
        Description = newDescription;
    }

    public void ChangeDueOn(DateTimeOffset newDueOn)
    {
        DueOn = newDueOn;
    }

    public void Cancel()
    {
        if (Status != PlannedMovementStatus.Active)
        {
            throw new InvalidOperationException("Planned movement must be active to be canceled.");
        }

        Status = PlannedMovementStatus.Canceled;
    }

    public void Realize()
    {
        if (Status != PlannedMovementStatus.Active)
        {
            throw new InvalidOperationException("Planned movement must be active to be realized.");
        }

        Status = PlannedMovementStatus.Realized;
    }
}
