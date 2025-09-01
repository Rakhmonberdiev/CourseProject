using System.Security.Claims;

namespace Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            var idValue = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(idValue))
                return null;
            return Guid.Parse(idValue);
        }
    }
}
