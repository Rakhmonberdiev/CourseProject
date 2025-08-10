using Domain.Inventories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class InventoryAccessConfiguration : IEntityTypeConfiguration<InventoryAccess>
    {
        public void Configure(EntityTypeBuilder<InventoryAccess> b)
        {
            b.HasKey(x => new { x.InventoryId, x.UserId });

            b.Property(x => x.CanWrite).IsRequired();

            b.HasOne(x => x.Inventory)
                .WithMany(i => i.AccessList)
                .HasForeignKey(x => x.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.User)
                .WithMany(u => u.InventoryAccesses)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
