using Domain.Users;

namespace Domain.Inventories
{
    public sealed class InventoryAccess
    {
        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; } = null!;

        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public bool CanWrite { get; set; } = true;
    }
}
