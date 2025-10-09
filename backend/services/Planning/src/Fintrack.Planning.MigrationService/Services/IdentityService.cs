using Fintrack.Planning.Application.Interfaces;

namespace Fintrack.Planning.MigrationService.Services;

public sealed class IdentityService : IIdentityService
{
    public Guid GetUserIdentity()
    {
        return Guid.Empty;
    }
}
