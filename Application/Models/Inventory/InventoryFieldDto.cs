namespace Application.Models.Inventory
{
    public sealed record InventoryFieldDto(
        Guid Id,
        string Title,
        string Type,     
        bool ShowInTable,
        int Order
    );
}
