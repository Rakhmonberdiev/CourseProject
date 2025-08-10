using Domain.Users;

namespace Domain.Inventories
{
    public sealed class DiscussionPost
    {
        public Guid Id { get; set; }

        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; } = null!;

        public Guid AuthorId { get; set; }
        public AppUser Author { get; set; } = null!;

        public string Markdown { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
