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

            var testUserId = await db.Users.Where(x => x.Email == "j.m.raxmonberdiyev@gmail.com").Select(x => x.Id).FirstAsync();
            var testUserId2 = await db.Users.Where(x => x.Email == "rakh@gmail.com").Select(x => x.Id).FirstAsync();

            if(testUserId == Guid.Empty || testUserId2 == Guid.Empty)
                throw new InvalidOperationException("Test users not found. Please run UserSeeder first.");
            var inv1 = new Inventory
            {
                Id = Guid.NewGuid(),
                OwnerId = testUserId,
                Title = "Demo Inventory",
                DescriptionMarkdown = """
                ## Демонстрационный инвентарь  
                Тестовый инвентарь для проверки работы:  
                - Пользовательские поля  
                - Лайки  
                - Теги
                """,
                ImageUrl = "https://placehold.co/400x400",
                IsPublicWritable = true,
                CreatedAt = DateTime.UtcNow

            };
            var inv1Field = new InventoryField
            {
                Id = Guid.NewGuid(),
                Inventory = inv1,
                Title = "Описание предмета",
                Type = FieldType.MultiLineText,
                ShowInTable = true,
                Order = 1,
                MaxLength = 500
            };
            inv1.Fields.Add(inv1Field);
            inv1.CustomIdFormat = new CustomIdFormat
            {
                Inventory = inv1,
                Elements = new List<CustomIdElement>
                {
                    new CustomIdElement
                    {
                        Id = Guid.NewGuid(),
                        InventoryFormat = null!, // EF подставит
                        InventoryId = inv1.Id,
                        Type = CustomIdElementType.FixedText,
                        Order = 1,
                        Text = "INV"
                    },
                    new CustomIdElement
                    {
                        Id = Guid.NewGuid(),
                        InventoryId = inv1.Id,
                        Type = CustomIdElementType.Random6Digits,
                        Order = 2,
                        SeparatorBefore = "-"
                    }
                }
            };
            var inv1Item = new InventoryItem
            {
                Id = Guid.NewGuid(),
                Inventory = inv1,
                CustomId = "INV-123456",
                CreatedById = testUserId,
                CreatedAt = DateTime.UtcNow,
                FieldValues = new List<ItemFieldValue>
                {
                    new ItemFieldValue
                    {
                        Id = Guid.NewGuid(),
                        Field = inv1Field,
                        Type = FieldType.MultiLineText,
                        StringValue = "Первый тестовый предмет"
                    }
                },
                Likes = new List<ItemLike>
                {
                    new ItemLike { UserId = testUserId },
                    new ItemLike { UserId = testUserId2 }
                }
            };
            inv1.Items.Add(inv1Item);
            inv1.DiscussionPosts.Add(new DiscussionPost
            {
                Id = Guid.NewGuid(),
                Inventory = inv1,
                AuthorId = testUserId,
                Markdown = "### Обсуждение\nТестовый комментарий для проверки дискуссий."
            });

            var tag1 = new Tag { Id = Guid.NewGuid(), Value = "Test" };
            inv1.Tags.Add(new InventoryTag { Inventory = inv1, Tag = tag1 });

            inv1.AccessList.Add(new InventoryAccess
            {
                Inventory = inv1,
                UserId = testUserId2,
                CanWrite = true
            });

            var inv2 = new Inventory
            {
                Id = Guid.NewGuid(),
                OwnerId = testUserId2,
                Title = "Office Equipment",
                DescriptionMarkdown = """
                ## Офисное оборудование  
                Учёт ноутбуков, принтеров, проекторов и других устройств.
                """,
                ImageUrl = "https://placehold.co/400x300",
                IsPublicWritable = false,
                CreatedAt = DateTime.UtcNow
            };

            var inv2Field = new InventoryField
            {
                Id = Guid.NewGuid(),
                Inventory = inv2,
                Title = "Серийный номер",
                Type = FieldType.SingleLineText,
                ShowInTable = true,
                Order = 1,
                MaxLength = 100
            };
            inv2.Fields.Add(inv2Field);

            inv2.CustomIdFormat = new CustomIdFormat
            {
                Inventory = inv2,
                Elements = new List<CustomIdElement>
                {
                    new CustomIdElement
                    {
                        Id = Guid.NewGuid(),
                        InventoryId = inv2.Id,
                        Type = CustomIdElementType.DateTimeUtc,
                        Order = 1,
                        DateTimeFormat = "yyyyMMdd"
                    },
                    new CustomIdElement
                    {
                        Id = Guid.NewGuid(),
                        InventoryId = inv2.Id,
                        Type = CustomIdElementType.Sequence,
                        Order = 2,
                        SeparatorBefore = "-"
                    }
                }
            };

            var inv2Item = new InventoryItem
            {
                Id = Guid.NewGuid(),
                Inventory = inv2,
                CustomId = "20250826-001",
                CreatedById = testUserId,
                CreatedAt = DateTime.UtcNow,
                FieldValues = new List<ItemFieldValue>
                {
                    new ItemFieldValue
                    {
                        Id = Guid.NewGuid(),
                        Field = inv2Field,
                        Type = FieldType.SingleLineText,
                        StringValue = "SN-ABC123456"
                    }
                }
            };
            inv2.Items.Add(inv2Item);
            inv2.DiscussionPosts.Add(new DiscussionPost
            {
                Id = Guid.NewGuid(),
                Inventory = inv2,
                AuthorId = testUserId,
                Markdown = "### Вопрос\nКакой принтер добавить в список первым?"
            });
            var tag2 = new Tag { Id = Guid.NewGuid(), Value = "Office" };
            inv2.Tags.Add(new InventoryTag { Inventory = inv2, Tag = tag2 });
            inv2.AccessList.Add(new InventoryAccess
            {
                Inventory = inv2,
                UserId = testUserId,
                CanWrite = true
            });
            await db.Inventories.AddRangeAsync(inv1, inv2);
            await db.SaveChangesAsync();
        }
    }
}
