using Domain.Items;
using Domain.Tags;
using Domain.Users;

namespace Domain.Inventories
{
    public sealed class Inventory
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }
        public AppUser Owner { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? DescriptionMarkdown { get; set; }
        public string? Category { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsPublicWritable { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public uint Xmin { get; private set; }
        public ICollection<InventoryAccess> AccessList { get; set; } = new List<InventoryAccess>();
        public ICollection<InventoryField> Fields { get; set; } = new List<InventoryField>();
        public CustomIdFormat? CustomIdFormat { get; set; }
        public ICollection<InventoryItem> Items { get; set; } = new List<InventoryItem>();
        public ICollection<DiscussionPost> DiscussionPosts { get; set; } = new List<DiscussionPost>();
        public ICollection<InventoryTag> Tags { get; set; } = new List<InventoryTag>();
    }
}
