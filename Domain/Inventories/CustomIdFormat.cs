namespace Domain.Inventories
{
    public sealed class CustomIdFormat
    {
        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; } = null!;

        public ICollection<CustomIdElement> Elements { get; set; } = new List<CustomIdElement>();
    }
}
