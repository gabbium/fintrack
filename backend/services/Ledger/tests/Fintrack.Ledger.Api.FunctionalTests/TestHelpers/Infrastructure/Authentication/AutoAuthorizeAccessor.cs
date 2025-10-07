namespace Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Infrastructure.Authentication;

public class AutoAuthorizeAccessor : IAutoAuthorizeAccessor
{
    public ClaimsPrincipal? User { get; set; }
}

