using Application.Abstractions.Discussion;

namespace Presentation.Endpoints.Discussion
{
    public static class GetAllDiscussionsEndpoint
    {
        public static RouteGroupBuilder MapGetAllDiscussions(this RouteGroupBuilder builder)
        {
            builder.MapGet("{invId:guid}", async (Guid invId,int page,int pageSize,IDiscussionRepository repo) =>
            {
                var result = await repo.GetInventoryPostsAsync(invId, page, pageSize);
                return Results.Ok(result);
            });
            return builder;
        }
    }
}
