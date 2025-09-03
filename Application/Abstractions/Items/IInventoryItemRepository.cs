

using Application.Common;
using Application.Models.Item;
using Domain.Items;

namespace Application.Abstractions.Items
{
    public interface IInventoryItemRepository
    {
        Task<Result> AddItemAsync(CreateInventoryItemRequest req, Guid userId, bool isAdmin);
    }
}
