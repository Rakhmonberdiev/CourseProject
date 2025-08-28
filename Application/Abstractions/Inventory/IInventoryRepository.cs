using Application.Common;
using Application.Models.Inventory;

namespace Application.Abstractions.Inventory
{
    public interface IInventoryRepository
    {
        Task<Result<PagedResult<InventoryDto>>> GetAllInventories(InventoryQuery query);
        Task<Result<InventoryDetailsDto>> GetInventoryById(Guid id);
        Task<Result<PagedResult<InventoryItemDto>>> GetItemByInvId(Guid invId, InventoryItemsQuery query);
    }
}
