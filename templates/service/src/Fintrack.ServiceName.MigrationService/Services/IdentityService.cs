using Fintrack.ServiceName.Application.Interfaces;

namespace Fintrack.ServiceName.MigrationService.Services;

public sealed class IdentityService : IIdentityService
{
    public Guid GetUserIdentity()
    {
        return Guid.Empty;
    }
}
