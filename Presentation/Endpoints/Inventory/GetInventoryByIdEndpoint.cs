using Application.Abstractions.Inventory;

namespace Presentation.Endpoints.Inventory
{
    public static class GetInventoryByIdEndpoint
    {
        public static RouteGroupBuilder MapGetInventoryByIdEndpoint(this RouteGroupBuilder app)
        {
            app.MapGet("{id:guid}", async (Guid id, IInventoryRepository repo) =>
            {
                var result = await repo.GetInventoryById(id);
                if (!result.Succeeded)
                    return Results.NotFound(new { error = result.Errors });

                return Results.Ok(result.Value);
            });

            return app;
        }
    }
}
