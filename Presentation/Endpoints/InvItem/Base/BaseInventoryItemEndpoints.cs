namespace Presentation.Endpoints.InvItem.Base
{
    public static class BaseInventoryItemEndpoints
    {
        public static IEndpointRouteBuilder MapInventoryItem(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/item").WithTags("InvItems");
            group.MapToggleItemLike();
                
            return app;
        }
    }
}
