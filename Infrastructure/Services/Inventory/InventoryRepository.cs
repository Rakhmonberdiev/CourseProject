using Application.Abstractions.Inventory;
using Application.Common;
using Application.Models.Inventory;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

namespace Infrastructure.Services.Inventory
{
    public sealed class InventoryRepository(AppDbContext db) : IInventoryRepository
    {
        public async Task<Result<PagedResult<InventoryDto>>> GetAllInventories(InventoryQuery query)
        {
            var baseQuery = db.Inventories.AsNoTracking();
            var projected = baseQuery.Select(i => new
            {
                i.Id,
                i.Title,
                i.ImageUrl,
                OwnerName = i.Owner.UserName!,
                ItemsCount = i.Items.Count,
                LikesCount = i.Items.SelectMany(it => it.Likes).Count(),
                CreatedAt = i.CreatedAt,
                LastItemAt = i.Items.Max(it => (DateTime?)it.CreatedAt),
                SearchVector = EF.Property<NpgsqlTsVector>(i, "SearchVector")
            });
            NpgsqlTsQuery? tsQuery = null;
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var qtext = query.Search!.Trim();
                tsQuery = EF.Functions.WebSearchToTsQuery("simple", qtext);

                projected = projected
                    .Where(x => x.SearchVector.Matches(tsQuery));
            }

            var queryable = projected;
            if (tsQuery is not null)
            {
                queryable = queryable
                    .OrderByDescending(x => x.SearchVector.Rank(EF.Functions.ToTsQuery(query.Search!)))
                    .ThenByDescending(x => x.LikesCount)
                    .ThenByDescending(x => x.ItemsCount)
                    .ThenByDescending(x => x.CreatedAt);
            }
            else
            {
                queryable = query.Sort switch
                {
                    InventorySort.Popular => projected
                        .OrderByDescending(x => x.LikesCount)
                        .ThenByDescending(x => x.ItemsCount)
                        .ThenByDescending(x => x.CreatedAt),

                    InventorySort.Recent => projected
                        .OrderByDescending(x => x.LastItemAt ?? x.CreatedAt)
                        .ThenByDescending(x => x.CreatedAt),

                    InventorySort.ItemsDesc => projected
                        .OrderByDescending(x => x.ItemsCount)
                        .ThenByDescending(x => x.CreatedAt),

                    InventorySort.CreatedAsc => projected
                        .OrderBy(x => x.CreatedAt),

                    _ => projected
                        .OrderByDescending(x => x.CreatedAt),
                };
            }
            var page = Math.Max(1, query.Page);
            var pageSize = Math.Clamp(query.PageSize, 1, 100);
            var skip = (page - 1) * pageSize;

            var totalCount = await queryable.CountAsync();
            var items = await queryable
                .Skip(skip)
                .Take(pageSize)
                .Select(i => new InventoryDto(
                    i.Id,
                    i.Title,
                    i.OwnerName,
                    i.ItemsCount,
                    i.LikesCount,
                    i.ImageUrl))
                .ToListAsync();
            var paged = new PagedResult<InventoryDto>(items, totalCount, page, pageSize);
            return Result<PagedResult<InventoryDto>>.Ok(paged);
        }
    }
}
