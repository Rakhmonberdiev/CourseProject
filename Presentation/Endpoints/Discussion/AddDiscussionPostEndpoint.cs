using Application.Abstractions.Discussion;
using Application.Models.Discussion;
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
            IDiscussionRepository repo) =>
            {
                Guid userId;
                var sub = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!Guid.TryParse(sub, out userId))
                    return Results.Unauthorized();
                var result = await repo.AddInventoryPostAsync(invId, userId, request.Markdown);

                if (!result.Succeeded) return Results.BadRequest(new { errors = result.Errors });
                return Results.Ok(result.Value);
            }).RequireAuthorization();

            return builder;
        }
    }
}
