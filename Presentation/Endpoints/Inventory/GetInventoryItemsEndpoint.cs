using Application.Abstractions.Inventory;
using Application.Abstractions.Items;
using Application.Models.Inventory;
using Infrastructure.Extensions;
using System.Security.Claims;

namespace Presentation.Endpoints.Inventory
{
    public static class GetInventoryItemsEndpoint
    {
        public static RouteGroupBuilder MapGetInventoryItemsEndpoint(this RouteGroupBuilder app)
        {
           
            app.MapGet("{id:guid}/items", async (Guid id, [AsParameters] InventoryItemsQuery query, IInventoryItemRepository repo,ClaimsPrincipal user) =>
            {
                var userId = user.GetUserId();
                var result = await repo.GetItemByInvId(id, query, userId);
                return result.Succeeded ? Results.Ok(result.Value) : Results.Problem();
            });
            return app;
        }
    }
}
