using Fintrack.Ledger.Application.Interfaces;

namespace Fintrack.Ledger.MigrationService.Services;

internal sealed class CurrentUser : IUser
{
    public Guid UserId => Guid.Empty;
}


