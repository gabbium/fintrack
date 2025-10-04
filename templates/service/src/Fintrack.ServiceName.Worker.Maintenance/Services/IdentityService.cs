using Fintrack.ServiceName.Application.Interfaces;

namespace Fintrack.ServiceName.Worker.Maintenance.Services;

public sealed class IdentityService : IIdentityService
{
    public Guid GetUserIdentity()
    {
        return Guid.Empty;
    }
}
