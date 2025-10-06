using Fintrack.Planning.Application.Interfaces;

namespace Fintrack.Planning.Api.Services;

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

