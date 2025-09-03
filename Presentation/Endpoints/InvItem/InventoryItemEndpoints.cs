using Application.Abstractions.Items;
using Application.Models.Item;
using System.Security.Claims;

namespace Presentation.Endpoints.InvItem
{
    public static class InventoryItemEndpoints
    {
        public static RouteGroupBuilder MapInventoryItems(this RouteGroupBuilder group)
        {
            group.MapPost("add-item", async (CreateInventoryItemRequest req, ClaimsPrincipal user, IInventoryItemRepository repo) =>
            {
                var sub = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!Guid.TryParse(sub, out var userId))
                    return Results.Unauthorized();
                var isAdmin = user.IsInRole("admin");
                var result = await repo.AddItemAsync(req, userId,isAdmin);
                if (!result.Succeeded)
                    return Results.BadRequest(new { errors = result.Errors });
                return Results.NoContent();
            }).RequireAuthorization();

            return group;
        }
    }
}
