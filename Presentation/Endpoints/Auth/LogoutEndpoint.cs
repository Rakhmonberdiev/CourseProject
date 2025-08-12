using Application.Abstractions.Auth;

namespace Presentation.Endpoints.Auth
{
    public static class LogoutEndpoint
    {
        public static RouteGroupBuilder MapLogout(this RouteGroupBuilder app)
        {
            app.MapPost("logout", async (IAuthService auth) =>
            {
                var r = await auth.LogoutAsync();
                return r.Succeeded
                    ? Results.Ok(new { message = r.Message })
                    : Results.BadRequest(new { errors = r.Errors });
            });

            return app;
        }
    }
}
