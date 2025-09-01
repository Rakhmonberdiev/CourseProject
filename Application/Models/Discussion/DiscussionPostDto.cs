namespace Application.Models.Discussion
{
    public sealed record DiscussionPostDto(
        Guid Id,
        Guid AuthorId,
        string AuthorName,
        string Markdown,
        DateTime CreatedAt
    );
}
