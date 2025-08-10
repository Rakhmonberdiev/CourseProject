using Domain.Common;
using Domain.Inventories;

namespace Domain.Items;

public sealed class ItemFieldValue
{
    public Guid Id { get; set; }

    public Guid ItemId { get; set; }
    public InventoryItem Item { get; set; } = null!;

    public Guid FieldId { get; set; }
    public InventoryField Field { get; set; } = null!;

    public FieldType Type { get; set; }

    public string? StringValue { get; set; }  
    public decimal? NumericValue { get; set; } 
    public bool? BoolValue { get; set; }  
}
