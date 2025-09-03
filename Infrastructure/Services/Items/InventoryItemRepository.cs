using Application.Abstractions.Items;
using Application.Common;
using Application.Models.Item;
using Domain.Items;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
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
    }
}
