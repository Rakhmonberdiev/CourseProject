using Domain.Users;

namespace Domain.Items;

public sealed class ItemLike
{
    public Guid ItemId { get; set; }
    public InventoryItem Item { get; set; } = null!;

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
