namespace Application.Common
{
    public sealed record PagedResult<T>(IEnumerable<T> items, int totalCount, int page,int pageSize);
}
