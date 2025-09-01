
namespace Application.Models.Inventory
{
    public sealed record InventoryQuery(
    int Page = 1,
    int PageSize = 12,
    bool MyOnly = false,
    string? Search = null
    );
}
