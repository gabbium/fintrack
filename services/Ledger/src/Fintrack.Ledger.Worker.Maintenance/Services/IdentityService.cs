using Fintrack.Ledger.Application.Interfaces;

namespace Fintrack.Ledger.Worker.Maintenance.Services;

public sealed class IdentityService : IIdentityService
{
    public Guid GetUserIdentity()
    {
        return Guid.Empty;
    }
}
