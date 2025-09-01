
namespace Application.Models.Inventory
{
    public sealed record InventoryItemDto(
        Guid Id,
        string CustomId,
        string CreatedByName,
        DateTime CreatedAt,
        int LikesCount,
        IReadOnlyList<ItemFieldValueDto> Fields
    );

    public sealed record ItemFieldValueDto(
        Guid FieldId,
        string FieldTitle,
        int Type,        
        string? StringValue,
        decimal? NumericValue,
        bool? BoolValue
    );
}
