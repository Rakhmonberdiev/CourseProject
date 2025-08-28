namespace Presentation.Endpoints.Inventory.Base
{
    public static class BaseInventoryEndpoints
    {
        public static IEndpointRouteBuilder MapInventory(this  IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/inventories").WithTags("Inventories");
            group
                .MapGetAllInventoriesEndpoint()
                .MapGetInventoryByIdEndpoint()
                .MapGetInventoryItemsEndpoint();
            return app;
        }
    }
}
