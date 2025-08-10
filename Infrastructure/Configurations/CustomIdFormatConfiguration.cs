using Domain.Inventories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class CustomIdFormatConfiguration : IEntityTypeConfiguration<CustomIdFormat>
    {
        public void Configure(EntityTypeBuilder<CustomIdFormat> b)
        {
            b.HasKey(x => x.InventoryId);

            b.HasOne(x => x.Inventory)
                .WithOne(i => i.CustomIdFormat)
                .HasForeignKey<CustomIdFormat>(x => x.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
