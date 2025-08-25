
namespace Application.Models.Inventory
{
    public sealed record InventoryDto(
        Guid Id,
        string Title,
        string OwnerName,
        int ItemsCount,
        int LikesCount, 
        string? ImageUrl
        );
}
