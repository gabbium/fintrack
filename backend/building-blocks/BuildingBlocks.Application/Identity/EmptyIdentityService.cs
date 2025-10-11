namespace BuildingBlocks.Application.Identity;

public sealed class EmptyIdentityService : IIdentityService
{
    public Guid GetUserIdentity()
    {
        return Guid.Empty;
    }
}
