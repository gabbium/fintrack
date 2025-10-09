namespace Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Infrastructure.Authentication;

public interface IAutoAuthorizeAccessor
{
    ClaimsPrincipal? User { get; set; }
}

