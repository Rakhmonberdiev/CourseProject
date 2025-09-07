

using Application.Common;
using Application.Models.Inventory;
using Application.Models.Item;
using Domain.Items;

namespace Application.Abstractions.Items
{
    public interface IInventoryItemRepository
    {
        Task<Result> AddItemAsync(CreateInventoryItemRequest req, Guid userId, bool isAdmin);
        Task<Result<ItemLikeDto>> ToggleItemLike(Guid itemId, Guid userId);
        Task<Result<PagedResult<InventoryItemDto>>> GetItemByInvId(Guid invId, InventoryItemsQuery query, Guid? userId = null);

    }
}
