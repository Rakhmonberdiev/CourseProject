using Application.Abstractions.Inventory;
using Infrastructure.Extensions;
using System.Security.Claims;

namespace Presentation.Endpoints.Inventory
{
    public static class GetInventoryByIdEndpoint
    {
        public static RouteGroupBuilder MapGetInventoryByIdEndpoint(this RouteGroupBuilder app)
        {
            app.MapGet("{id:guid}", async (Guid id, IInventoryRepository repo, ClaimsPrincipal user) =>
            {
                var userId = user.GetUserId();
                var result = await repo.GetInventoryById(id,userId);
                if (!result.Succeeded)
                    return Results.NotFound(new { error = result.Errors });

                return Results.Ok(result.Value);
            });

            return app;
        }
    }
}
