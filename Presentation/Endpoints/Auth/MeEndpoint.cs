using Application.Abstractions.Auth;
using Application.Common;
using System.Security.Claims;

namespace Presentation.Endpoints.Auth
{
    public static class MeEndpoint
    {
        public static RouteGroupBuilder MapUserInfo(this RouteGroupBuilder app)
        {
            app.MapGet("me", async (ClaimsPrincipal user,IAuthService authService) =>
            {
                var result = await authService.UserInfo(user);
                return result.Succeeded ? Results.Ok(result.Value) : Results.Unauthorized();
            }).RequireAuthorization();

            return app;
        }
    }
}
