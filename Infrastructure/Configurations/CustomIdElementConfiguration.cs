using Domain.Inventories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class CustomIdElementConfiguration : IEntityTypeConfiguration<CustomIdElement>
    {
        public void Configure(EntityTypeBuilder<CustomIdElement> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Order).IsRequired();

            b.Property(x => x.Text).HasMaxLength(64);

            b.Property(x => x.NumberFormat).HasMaxLength(16);

            b.Property(x => x.DateTimeFormat).HasMaxLength(32);

            b.Property(x => x.SeparatorBefore).HasMaxLength(8);

            b.HasIndex(x => new { x.InventoryId, x.Order });

            b.HasOne(x => x.InventoryFormat)
                .WithMany(f => f.Elements)
                .HasForeignKey(x => x.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
