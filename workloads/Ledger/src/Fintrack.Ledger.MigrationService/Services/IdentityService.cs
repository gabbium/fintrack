using Fintrack.Ledger.Application.Interfaces;

namespace Fintrack.Ledger.MigrationService.Services;

internal sealed class IdentityService : IIdentityService
{
    public Guid GetUserIdentity()
    {
        return Guid.Empty;
    }
}


