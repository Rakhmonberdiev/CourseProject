using Application.Abstractions.Inventory;
using Application.Models.Inventory;

namespace Presentation.Endpoints.Inventory
{
    public static class GetAllInventoriesEndpoint
    {
        public static RouteGroupBuilder MapGetAllInventoriesEndpoint(this RouteGroupBuilder app)
        {
            app.MapGet("", async ([AsParameters]InventoryQuery query, IInventoryRepository repo) =>
            {
                var result = await repo.GetAllInventories(query);
                return result.Succeeded ? Results.Ok(result.Value) : Results.Problem();
            });
            return app;
        }
    }
}
