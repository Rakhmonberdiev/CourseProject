using Application.Common;
using Application.Models.Discussion;

namespace Application.Abstractions.Discussion
{
    public interface IDiscussionRepository
    {
        Task<PagedResult<DiscussionPostDto>> GetInventoryPostsAsync(Guid inventoryId, int page, int pageSize);
        Task<Result<DiscussionPostDto>> AddInventoryPostAsync(Guid inventoryId, Guid authorId, string markdown);
    }
}
