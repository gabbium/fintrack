namespace Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Infrastructure.Authorize;

public interface IAutoAuthorizeAccessor
{
    ClaimsPrincipal? User { get; set; }
}

