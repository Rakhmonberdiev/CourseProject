using Domain.Inventories;
using Domain.Users;

namespace Domain.Items
{
    public sealed class InventoryItem
    {
        public Guid Id { get; set; }

        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; } = null!;
        public string CustomId { get; set; } = null!; 

        public Guid CreatedById { get; set; }
        public AppUser CreatedBy { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public uint Xmin { get; private set; }

        public ICollection<ItemFieldValue> FieldValues { get; set; } = new List<ItemFieldValue>();
        public ICollection<ItemLike> Likes { get; set; } = new List<ItemLike>();
    }
}
