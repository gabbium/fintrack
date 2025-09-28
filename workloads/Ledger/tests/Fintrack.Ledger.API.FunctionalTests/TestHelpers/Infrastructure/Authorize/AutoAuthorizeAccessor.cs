namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Authorize;

public sealed class AutoAuthorizeAccessor : IAutoAuthorizeAccessor
{
    public ClaimsPrincipal? User { get; set; }
}
