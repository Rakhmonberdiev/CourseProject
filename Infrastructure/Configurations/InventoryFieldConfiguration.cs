using Domain.Inventories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class InventoryFieldConfiguration : IEntityTypeConfiguration<InventoryField>
    {
        public void Configure(EntityTypeBuilder<InventoryField> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Title)
                .HasMaxLength(128)
                .IsRequired();

            b.Property(x => x.ShowInTable).IsRequired();

            b.Property(x => x.Order).IsRequired();

            b.HasIndex(x => new { x.InventoryId, x.Order });

            b.HasOne(x => x.Inventory)
                .WithMany(i => i.Fields)
                .HasForeignKey(x => x.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
