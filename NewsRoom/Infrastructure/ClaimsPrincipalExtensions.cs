using System.Security.Claims;

namespace NewsRoom.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
            => user .FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
