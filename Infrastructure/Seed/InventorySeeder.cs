using Domain.Common;
using Domain.Inventories;
using Domain.Items;
using Domain.Tags;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed
{
    public sealed class InventorySeeder
    {
        public async Task SeedAsync(AppDbContext db)
        {
            if (db.Inventories.Any()) return;

            var testUserId = await db.Users.Where(x => x.Email == "admin@gmail.com").Select(x => x.Id).FirstAsync();
            var testUserId2 = await db.Users.Where(x => x.Email == "test@gmail.com").Select(x => x.Id).FirstAsync();

            if(testUserId == Guid.Empty || testUserId2 == Guid.Empty)
                throw new InvalidOperationException("Test users not found. Please run UserSeeder first.");
            var rnd = new Random();
            var inventories = new List<Inventory>();

            for (int i = 1; i <= 12; i++)
            {
                var ownerId = i % 2 == 0 ? testUserId2 : testUserId;
                var inv = new Inventory
                {
                    Id = Guid.NewGuid(),
                    OwnerId = ownerId,
                    Title = $"Inventory #{i}",
                    DescriptionMarkdown = $"## Инвентарь {i}\nСгенерированный тестовый инвентарь.",
                    ImageUrl = "https://placehold.co/400x300",
                    IsPublicWritable = i % 2 == 0,
                    CreatedAt = DateTime.UtcNow
                };
                var field = new InventoryField
                {
                    Id = Guid.NewGuid(),
                    Inventory = inv,
                    Title = "Описание предмета",
                    Type = FieldType.MultiLineText,
                    ShowInTable = true,
                    Order = 1,
                    MaxLength = 500
                };
                inv.Fields.Add(field);

                inv.CustomIdFormat = new CustomIdFormat
                {
                    Inventory = inv,
                    Elements = new List<CustomIdElement>
                    {
                        new CustomIdElement
                        {
                            Id = Guid.NewGuid(),
                            InventoryId = inv.Id,
                            Type = CustomIdElementType.FixedText,
                            Order = 1,
                            Text = "INV"
                        },
                        new CustomIdElement
                        {
                            Id = Guid.NewGuid(),
                            InventoryId = inv.Id,
                            Type = CustomIdElementType.Random6Digits,
                            Order = 2,
                            SeparatorBefore = "-"
                        }
                    }
                };
                for(int j = 1; j <= 12; j++)
                {
                    var item = new InventoryItem
                    {
                        Id = Guid.NewGuid(),
                        Inventory = inv,
                        CustomId = $"INV-{i:D2}{j:D3}",
                        CreatedById = (j % 2 == 0 ? testUserId : testUserId2),
                        CreatedAt = DateTime.UtcNow.AddMinutes(-rnd.Next(0, 1000)),
                        FieldValues = new List<ItemFieldValue>
                        {
                            new ItemFieldValue
                            {
                                Id = Guid.NewGuid(),
                                Field = field,
                                Type = FieldType.MultiLineText,
                                StringValue = $"Описание предмета {j} в инвентаре {i}"
                            }
                        },
                        Likes = new List<ItemLike>
                        {
                            new ItemLike { UserId = testUserId },
                            new ItemLike { UserId = testUserId2 }
                        }
                    };
                    inv.Items.Add(item);
                }
                inv.DiscussionPosts.Add(new DiscussionPost
                {
                    Id = Guid.NewGuid(),
                    Inventory = inv,
                    AuthorId = ownerId,
                    Markdown = $"### Обсуждение\nКомментарий для инвентаря {i}"
                });

     
                for(int t=1; t<=5; t++)
                {
                    var tag = new Tag { Id = Guid.NewGuid(), Value = $"Tag-{i}-{t}" };
                    inv.Tags.Add(new InventoryTag { Inventory = inv, Tag = tag });
                }
                
                inv.AccessList.Add(new InventoryAccess
                {
                    Inventory = inv,
                    UserId = ownerId == testUserId ? testUserId2 : testUserId,
                    CanWrite = true
                });
                inventories.Add(inv);

            }
            await db.Inventories.AddRangeAsync(inventories);
            await db.SaveChangesAsync();
        }
    }
}
