using Domain.Common;

namespace Domain.Inventories
{
    public sealed class InventoryField
    {
        public Guid Id { get; set; }

        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public FieldType Type { get; set; }

        public bool ShowInTable { get; set; }
        public int Order { get; set; }
        public int? MaxLength { get; set; }
        public string? Regex { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
    }
}
