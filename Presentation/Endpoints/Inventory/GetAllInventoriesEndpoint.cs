using Application.Abstractions.Inventory;
using Application.Models.Inventory;
using Infrastructure.Extensions;
using System.Security.Claims;

namespace Presentation.Endpoints.Inventory
{
    public static class GetAllInventoriesEndpoint
    {
        public static RouteGroupBuilder MapGetAllInventoriesEndpoint(this RouteGroupBuilder app)
        {
            app.MapGet("", async ([AsParameters]InventoryQuery query, IInventoryRepository repo, ClaimsPrincipal user) =>
            {
                var userId = user.GetUserId();
                var result = await repo.GetAllInventories(query, userId);
                return result.Succeeded ? Results.Ok(result.Value) : Results.Problem();
            });
            return app;
        }
    }
}
