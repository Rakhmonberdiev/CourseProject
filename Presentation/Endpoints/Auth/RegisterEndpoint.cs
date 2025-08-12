using Application.Abstractions.Auth;
using Application.Models.Auth;

namespace Presentation.Endpoints.Auth
{
    public static class RegisterEndpoint
    {
        public  static RouteGroupBuilder MapRegister(this RouteGroupBuilder app)
        {
            app.MapPost("register", async (RegisterRequest req, IAuthService auth ) =>
            {
                var r = await auth.RegisterAsync(req);
                return r.Succeeded
                    ? Results.Ok(r.Value)
                    : Results.BadRequest(new { errors = r.Errors });
            });

            return app;
        }
    }
}
