namespace Fintrack.ServiceName.Api.FunctionalTests.TestHelpers.Infrastructure.Authorize;

public class AutoAuthorizeAccessor : IAutoAuthorizeAccessor
{
    public ClaimsPrincipal? User { get; set; }
}

