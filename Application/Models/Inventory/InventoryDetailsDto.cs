namespace Application.Models.Inventory
{
    public sealed record InventoryDetailsDto(
        Guid Id,
        string Title,
        string OwnerName,
        string? DescriptionMarkdown,
        string? ImageUrl,
        DateTime CreatedAt,
        int ItemsCount,
        int LikesCount,
        bool HasAccess,
        IReadOnlyList<string> Tags,
        IReadOnlyList<InventoryFieldDto> Fields
    );
}
