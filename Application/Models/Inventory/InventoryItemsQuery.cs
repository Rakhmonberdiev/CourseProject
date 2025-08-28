

namespace Application.Models.Inventory
{
    public enum InventoryItemsSort
    {
        CreatedDesc,  
        CreatedAsc,
        Popular  
    }
    public sealed record InventoryItemsQuery(
    int Page = 1,
    int PageSize = 10,
    InventoryItemsSort Sort = InventoryItemsSort.CreatedDesc);
}
