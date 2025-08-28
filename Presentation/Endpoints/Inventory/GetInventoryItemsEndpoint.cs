using Application.Abstractions.Inventory;
using Application.Models.Inventory;

namespace Presentation.Endpoints.Inventory
{
    public static class GetInventoryItemsEndpoint
    {
        public static RouteGroupBuilder MapGetInventoryItemsEndpoint(this RouteGroupBuilder app)
        {
           
            app.MapGet("{id:guid}/items", async (Guid id, [AsParameters] InventoryItemsQuery query, IInventoryRepository repo) =>
            {
                var result = await repo.GetItemByInvId(id, query);
                return result.Succeeded ? Results.Ok(result.Value) : Results.Problem();
            });
            return app;
        }
    }
}
