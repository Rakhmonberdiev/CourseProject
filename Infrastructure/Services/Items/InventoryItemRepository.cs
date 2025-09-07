using Application.Abstractions.Items;
using Application.Common;
using Application.Models.Inventory;
using Application.Models.Item;
using Domain.Items;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Items
{
    public class InventoryItemRepository(AppDbContext db) : IInventoryItemRepository
    {
        public async Task<Result> AddItemAsync(CreateInventoryItemRequest req , Guid userId, bool isAdmin)
        {

            var inventory = await db.Inventories.Include(x=>x.AccessList).FirstOrDefaultAsync(x=>x.Id == req.InventoryId);
            
            if(inventory is null) return Result.Fail("Инвентарь не найден");
            if (!isAdmin)
            {
                var canWrite = inventory.OwnerId == userId || inventory.IsPublicWritable || inventory.AccessList.Any(a => a.UserId == userId && a.CanWrite);
                if (!canWrite)
                    return Result.Fail("Нет прав на добавление элемента в этот инвентарь");
            }
            var item = new InventoryItem
            {
                InventoryId = req.InventoryId,
                CreatedById = userId,
                CustomId = req.CustomId
            };
            foreach (var fv in req.FieldValues)
            {
                item.FieldValues.Add(new ItemFieldValue
                {
                    Item = item,
                    FieldId = fv.FieldId,
                    Type = fv.Type,
                    StringValue = fv.StringValue,
                    NumericValue = fv.NumericValue,
                    BoolValue = fv.BoolValue
                });
            }
            db.InventoryItems.Add(item);
            await db.SaveChangesAsync();
            return Result.Ok();

        }
        public async Task<Result<PagedResult<InventoryItemDto>>> GetItemByInvId(Guid invId, InventoryItemsQuery query, Guid? userId = null)
        {
            var baseQuery = db.InventoryItems
            .AsNoTracking()
            .Where(it => it.InventoryId == invId);


            baseQuery = query.Sort switch
            {
                InventoryItemsSort.CreatedAsc => baseQuery.OrderBy(it => it.CreatedAt),

                InventoryItemsSort.Popular => baseQuery.OrderByDescending(it => it.Likes.Count).ThenByDescending(it => it.CreatedAt),

                _ => baseQuery.OrderByDescending(it => it.CreatedAt)
            };
            var total = await baseQuery.CountAsync();

            var items = await baseQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(i => new InventoryItemDto(
                    i.Id,
                    i.CustomId,
                    i.CreatedBy.UserName ?? "",
                    i.CreatedAt,
                    i.Likes.Count,
                    userId.HasValue && i.Likes.Any(l => l.UserId == userId.Value),
                    i.FieldValues
                        .Select(fv => new ItemFieldValueDto(
                            fv.FieldId,
                            fv.Field.Title,
                            fv.Type,
                            fv.StringValue,
                            fv.NumericValue,
                            fv.BoolValue))
                        .ToList()
                    )).ToListAsync();

            var paged = new PagedResult<InventoryItemDto>(items, total, query.Page, query.PageSize);
            return Result<PagedResult<InventoryItemDto>>.Ok(paged);
        }
        public async Task<Result<ItemLikeDto>> ToggleItemLike(Guid itemId, Guid userId)
        {
            var exists = await db.InventoryItems.AnyAsync(x => x.Id == itemId);
            if (!exists)
                return Result<ItemLikeDto>.Fail("Элемент не найден");
            var like = await db.ItemLikes.FirstOrDefaultAsync(x => x.ItemId == itemId && x.UserId == userId);
            if (like is null)
            {
                db.ItemLikes.Add(new ItemLike
                {
                    ItemId = itemId,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                });

                await db.SaveChangesAsync();
                return Result<ItemLikeDto>.Ok(new ItemLikeDto(itemId, false));
            }
            else
            {
                db.ItemLikes.Remove(like);

                await db.SaveChangesAsync();
                return Result<ItemLikeDto>.Ok(new ItemLikeDto(itemId, true));
            }

        }
    }
}
