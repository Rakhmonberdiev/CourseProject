using Application.Common;
using Application.Models.Inventory;

namespace Application.Abstractions.Inventory
{
    public interface IInventoryRepository
    {
        Task<Result<PagedResult<InventoryDto>>> GetAllInventories(InventoryQuery query);
    }
}
