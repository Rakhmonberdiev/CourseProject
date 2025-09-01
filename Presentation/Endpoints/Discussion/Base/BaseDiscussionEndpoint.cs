namespace Presentation.Endpoints.Discussion.Base
{
    public static class BaseDiscussionEndpoint
    {
        public static IEndpointRouteBuilder MapDiscussions(this IEndpointRouteBuilder app)
        {
            var gr = app.MapGroup("api/discussion").WithTags("discussion");

            gr.MapGetAllDiscussions()
                .MapAddDiscussionPost();
            return app;
        }
    }
}
