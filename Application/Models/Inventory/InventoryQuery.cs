
namespace Application.Models.Inventory
{
    public enum InventorySort
    {
        Popular,  
        Recent,    
        ItemsDesc,   
        CreatedDesc,
        CreatedAsc   
    }

    public sealed record InventoryQuery(
    int Page = 1,
    int PageSize = 12,
    string? Search = null, 
    InventorySort Sort = InventorySort.Popular
    );
}
