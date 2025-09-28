namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Authorize;

public interface IAutoAuthorizeAccessor
{
    ClaimsPrincipal? User { get; set; }
}
