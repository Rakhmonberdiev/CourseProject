using Domain.Inventories;
using Domain.Items;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users
{
    public sealed class AppUser : IdentityUser<Guid>
    {
        public string? DisplayName { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Inventory> OwnedInventories { get; set; } = new List<Inventory>();
        public ICollection<InventoryAccess> InventoryAccesses { get; set; } = new List<InventoryAccess>();
        public ICollection<InventoryItem> CreatedItems { get; set; } = new List<InventoryItem>();
        public ICollection<ItemLike> Likes { get; set; } = new List<ItemLike>();
    }
}
