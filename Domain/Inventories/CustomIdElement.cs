using Domain.Common;
namespace Domain.Inventories
{
    public sealed class CustomIdElement
    {
        public Guid Id { get; set; }

        public Guid InventoryId { get; set; }
        public CustomIdFormat InventoryFormat { get; set; } = null!;

        public CustomIdElementType Type { get; set; }
        public int Order { get; set; }

        public string? Text { get; set; }        
        public string? NumberFormat { get; set; }   
        public string? DateTimeFormat { get; set; } 
        public string? SeparatorBefore { get; set; } 
    }
}
