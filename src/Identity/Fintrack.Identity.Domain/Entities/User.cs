namespace Fintrack.Identity.Domain.Entities;

public sealed class User(string email) : BaseEntity, IAggregateRoot
{
    public string Email { get; private set; } = email;
}
