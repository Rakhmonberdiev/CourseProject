using Application.Abstractions.Discussion;
using Application.Common;
using Application.Models.Discussion;
using Domain.Inventories;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Services.Discussion
{
    public sealed class DiscussionRepository(AppDbContext db) : IDiscussionRepository
    {
        public async Task<Result<DiscussionPostDto>> AddInventoryPostAsync(Guid inventoryId, Guid authorId, string markdown)
        {
            var exists = await db.Inventories
                .AsNoTracking()
                .AnyAsync(i => i.Id == inventoryId);
            if (!exists)
                return Result<DiscussionPostDto>.Fail("Inventory not found.");

            var entity = new DiscussionPost
            {
                Id = Guid.NewGuid(),
                InventoryId = inventoryId,
                AuthorId = authorId,
                Markdown = markdown,
                CreatedAt = DateTime.UtcNow
            };
            db.DiscussionPosts.Add(entity);
            await db.SaveChangesAsync();
            var dto = await db.DiscussionPosts
                .AsNoTracking()
                .Where(p => p.Id == entity.Id)
                .Select(p => new DiscussionPostDto(
                    p.Id,
                    p.AuthorId,
                    p.Author.UserName!,
                    p.Markdown,
                    p.CreatedAt))
                .SingleAsync();
            return Result<DiscussionPostDto>.Ok(dto);
        }

        public async Task<PagedResult<DiscussionPostDto>> GetInventoryPostsAsync(Guid inventoryId, int page, int pageSize)
        {
            var baseQuery = db.DiscussionPosts.AsNoTracking().Where(p => p.InventoryId == inventoryId);
            var total = await baseQuery.CountAsync();

            var items = await baseQuery
                .OrderByDescending(x => x.CreatedAt)
                .ThenByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new DiscussionPostDto(
                    x.Id,
                    x.AuthorId,
                    x.Author.UserName ?? "",
                    x.Markdown,
                    x.CreatedAt
                    )).ToListAsync();

            return new PagedResult<DiscussionPostDto>(items, total, page, pageSize);
        }
    }
}
