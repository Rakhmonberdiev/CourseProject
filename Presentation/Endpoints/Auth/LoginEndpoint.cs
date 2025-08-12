using Application.Abstractions.Auth;
using Application.Models.Auth;

namespace Presentation.Endpoints.Auth
{
    public static class LoginEndpoint 
    {
        public static RouteGroupBuilder MapLoginEndpoint(this RouteGroupBuilder app)
        {
            app.MapPost("login", async (LoginRequest req, IAuthService auth) =>
            {
                var result = await auth.LoginAsync(req);
                return result.Succeeded ? Results.Ok(result.Value) : Results.Unauthorized();
            });

            return app;
        }
    }
}
