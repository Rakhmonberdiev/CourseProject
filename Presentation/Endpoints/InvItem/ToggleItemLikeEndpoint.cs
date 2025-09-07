using Application.Abstractions.Items;
using Infrastructure.Extensions;
using System.Security.Claims;

namespace Presentation.Endpoints.InvItem
{
    public static class ToggleItemLikeEndpoint
    {
        public static  RouteGroupBuilder MapToggleItemLike(this RouteGroupBuilder app)
        {
            app.MapPost("like/{itemId:guid}", async (Guid itemId, IInventoryItemRepository repo, ClaimsPrincipal user) =>
            {

                var userId = user.GetUserId();
                if (userId is null) return Results.Unauthorized();
                
                var toggleLike = await repo.ToggleItemLike(itemId,userId.Value);
                if (!toggleLike.Succeeded)
                    return Results.Problem( "Не удалось изменить лайк");
                return Results.Ok(toggleLike.Value);
            }).RequireAuthorization();

            return app;
        }
    }
}
