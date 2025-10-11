using BuildingBlocks.Application.Identity;

namespace BuildingBlocks.Api.Authentication.Identity;

public sealed class IdentityService(IHttpContextAccessor accessor) : IIdentityService
{
    public Guid GetUserIdentity()
    {
        var userId = accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(userId, out Guid parsedUserId)
            ? parsedUserId
            : Guid.Empty;
    }
}

