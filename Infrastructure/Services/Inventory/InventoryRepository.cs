using Application.Abstractions.Inventory;
using Application.Common;
using Application.Models.Inventory;
using Domain.Inventories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Inventory
{
    public sealed class InventoryRepository(AppDbContext db) : IInventoryRepository
    {
        public async Task<Result<PagedResult<InventoryDto>>> GetAllInventories(InventoryQuery query,Guid? userId)
        {
            var baseQuery = db.Inventories.OrderByDescending(x => x.CreatedAt).AsNoTracking();
            if(userId is not null && query.MyOnly)
            {
                baseQuery = baseQuery.Where(x => x.OwnerId == userId);
            }
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var searchTerms = query.Search.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var prefixSearchQuery = string.Join(" & ", searchTerms.Select(term => $"{term}:*"));
                baseQuery = baseQuery.Where(x => x.SearchVector.Matches(EF.Functions.ToTsQuery(prefixSearchQuery)));
            }
            var totalItems = await baseQuery.CountAsync();
            var inventories = await baseQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(x => new InventoryDto(
                    x.Id,
                    x.Title,
                    x.Owner.UserName ?? "",
                    x.Items.Count,
                    x.Items.SelectMany(i => i.Likes).Count(),
                    x.ImageUrl
                ))
                .ToListAsync();

            var pagedResult = new PagedResult<InventoryDto>(inventories, totalItems, query.Page, query.PageSize);
            return Result<PagedResult<InventoryDto>>.Ok(pagedResult);
        }

        public async Task<Result<InventoryDetailsDto>> GetInventoryById(Guid id, Guid? userId)
        {
            var dto = await db.Inventories
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new InventoryDetailsDto(
                    x.Id,
                    x.Title,
                    x.Owner.UserName ?? "",
                    x.DescriptionMarkdown,
                    x.ImageUrl,
                    x.CreatedAt,
                    x.Items.Count,
                    x.Items.SelectMany(item => item.Likes).Count(),
                    userId.HasValue && (x.Owner.Id == userId.Value || x.AccessList.Any(a => a.UserId == userId.Value)),
                    x.Tags.Select(t => t.Tag.Value).ToList(),
                    x.Fields
                        .OrderBy(f => f.Order)
                        .Select(f => new InventoryFieldDto(
                            f.Id,
                            f.Title,
                            f.Type.ToString(),
                            f.ShowInTable,
                            f.Order))
                        .ToList()

                    )).FirstOrDefaultAsync();

            if(dto is null)
                return Result<InventoryDetailsDto>.Fail("Inventory not found");

            return Result<InventoryDetailsDto>.Ok(dto);
        }


    }
}
