namespace Application.Models.Item
{
    public sealed record CreateInventoryItemRequest(
        Guid InventoryId,
        string CustomId,   
        IEnumerable<ItemFieldValueDto> FieldValues
    );
}
