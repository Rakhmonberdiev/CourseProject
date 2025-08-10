using Domain.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class InventoryTagConfiguration : IEntityTypeConfiguration<InventoryTag>
    {
        public void Configure(EntityTypeBuilder<InventoryTag> b)
        {
            b.HasKey(x => new { x.InventoryId, x.TagId });

            b.HasOne(x => x.Inventory)
                .WithMany(i => i.Tags)
                .HasForeignKey(x => x.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Tag)
                .WithMany(t => t.Inventories)
                .HasForeignKey(x => x.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
