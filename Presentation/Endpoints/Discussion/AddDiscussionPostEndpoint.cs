using Application.Abstractions.Discussion;
using Application.Models.Discussion;
using Microsoft.AspNetCore.SignalR;
using Presentation.Hubs;
using System.Security.Claims;

namespace Presentation.Endpoints.Discussion
{
    public static class AddDiscussionPostEndpoint
    {
        public static RouteGroupBuilder MapAddDiscussionPost(this RouteGroupBuilder builder)
        {
            builder.MapPost("{invId:guid}", async (Guid invId,
            CreateDiscussionPostRequest request,
            ClaimsPrincipal user,
            IDiscussionRepository repo,
            IHubContext<DiscussionHub> hubContext) =>
            {
                Guid userId;
                var sub = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!Guid.TryParse(sub, out userId))
                    return Results.Unauthorized();
                var result = await repo.AddInventoryPostAsync(invId, userId, request.Markdown);

                if (!result.Succeeded) return Results.BadRequest(new { errors = result.Errors });
                await hubContext.Clients.Group(invId.ToString()).SendAsync("ReceivePost", result.Value);
                return Results.Ok();
            }).RequireAuthorization();

            return builder;
        }
    }
}
