using Application.Common;
using Application.Models.Inventory;

namespace Application.Abstractions.Inventory
{
    public interface IInventoryRepository
    {
        Task<Result<PagedResult<InventoryDto>>> GetAllInventories(InventoryQuery query, Guid? userId);
        Task<Result<InventoryDetailsDto>> GetInventoryById(Guid id, Guid? userId);
        Task<Result<PagedResult<InventoryItemDto>>> GetItemByInvId(Guid invId, InventoryItemsQuery query);
    }
}
