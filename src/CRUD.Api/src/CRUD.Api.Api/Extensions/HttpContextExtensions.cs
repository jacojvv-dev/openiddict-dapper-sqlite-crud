using OpenIddict.Abstractions;

namespace CRUD.Api.Api.Extensions;

public static class HttpContextExtensions
{
    public static Guid GetUserId(this HttpContext? context)
    {
        if (context == default)
            throw new Exception("Failed to determine user id. Context was null.");

        var subjectClaim = context.User.Claims.FirstOrDefault(c => c.Type == OpenIddictConstants.Claims.Subject);
        if (subjectClaim == default)
            throw new Exception("Failed to determine user id. No subject claim was available for the request.");

        var claimValue = subjectClaim.Value;
        var hasValidUserId = Guid.TryParse(claimValue, out var userId);
        if (!hasValidUserId)
            throw new Exception("Failed to determine user id. The subject claim is not a well formed guid.");

        return userId;
    }
}