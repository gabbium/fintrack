using Fintrack.Ledger.Application.Interfaces;

namespace Fintrack.API.FunctionalTests.TestHelpers.Services;

public class EmptyUser : IUser
{
    public Guid Id => Guid.Empty;
}
