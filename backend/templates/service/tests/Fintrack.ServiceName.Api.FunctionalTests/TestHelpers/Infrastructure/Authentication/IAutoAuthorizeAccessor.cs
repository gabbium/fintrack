namespace Fintrack.ServiceName.Api.FunctionalTests.TestHelpers.Infrastructure.Authentication;

public interface IAutoAuthorizeAccessor
{
    ClaimsPrincipal? User { get; set; }
}

