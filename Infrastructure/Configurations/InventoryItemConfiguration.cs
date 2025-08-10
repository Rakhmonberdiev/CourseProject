using Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
    {
        public void Configure(EntityTypeBuilder<InventoryItem> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.CustomId).IsRequired();


            b.HasIndex(x => new { x.InventoryId, x.CustomId }).IsUnique();

            b.Property(x => x.CreatedAt).IsRequired();

            b.Property(x => x.Xmin).IsRowVersion();

            b.HasOne(x => x.Inventory)
                .WithMany(i => i.Items)
                .HasForeignKey(x => x.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.CreatedBy)
                .WithMany(u => u.CreatedItems)
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
