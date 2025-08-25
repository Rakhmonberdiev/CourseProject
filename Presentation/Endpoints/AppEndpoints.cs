using Presentation.Endpoints.Auth.Base;
using Presentation.Endpoints.Inventory.Base;

namespace Presentation.Endpoints
{
    public static class AppEndpoints
    {
        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapAuth();
            app.MapInventory();
            return app;
        }
    }
}
