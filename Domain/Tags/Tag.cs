namespace Domain.Tags;

public sealed class Tag
{
    public Guid Id { get; set; }
    public string Value { get; set; } = null!; // normalized lowercase

    public ICollection<InventoryTag> Inventories { get; set; } = new List<InventoryTag>();
}
