namespace BuildingBlocks.Api.FunctionalTests.AutoAuthorize;

public interface IAutoAuthorizeAccessor
{
    ClaimsPrincipal? Current { get; }

    void Impersonate(ClaimsPrincipal? user);
}

