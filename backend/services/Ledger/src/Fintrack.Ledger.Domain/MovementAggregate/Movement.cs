namespace Fintrack.Ledger.Domain.MovementAggregate;

public sealed class Movement : BaseEntity, IAggregateRoot
{
    public Guid UserId { get; private set; }
    public MovementKind Kind { get; private set; }
    public decimal Amount { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset OccurredOn { get; private set; }

    public Movement(
        Guid userId,
        MovementKind kind,
        decimal amount,
        string? description,
        DateTimeOffset occurredOn)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("User id cannot be empty.", nameof(userId));
        }

        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }

        UserId = userId;
        Kind = kind;
        Amount = amount;
        Description = description;
        OccurredOn = occurredOn;
    }

    public void ChangeKind(MovementKind newKind)
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

    public void ChangeOccurredOn(DateTimeOffset newOccurredOn)
    {
        OccurredOn = newOccurredOn;
    }
}
