using Presentation.Endpoints.Auth.Base;

namespace Presentation.Endpoints
{
    public static class AppEndpoints
    {
        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapAuth();
            return app;
        }
    }
}
