using Domain.Inventories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Configurations
{
    public sealed class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired();

            builder.Property(x => x.IsPublicWritable)
                .IsRequired();

            builder.HasIndex(x => x.OwnerId);

            builder.HasIndex(x => x.CreatedAt);

            builder.Property(x => x.Xmin)
                .IsRowVersion();

            builder.HasOne(i => i.Owner)
                .WithMany(u => u.OwnedInventories)
                .HasForeignKey(i => i.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.CustomIdFormat)
                .WithOne(f => f.Inventory)
                .HasForeignKey<CustomIdFormat>(f => f.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.Fields)
                .WithOne(f => f.Inventory)
                .HasForeignKey(f => f.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.Items)
                .WithOne(it => it.Inventory)
                .HasForeignKey(it => it.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.DiscussionPosts)
                .WithOne(dp => dp.Inventory)
                .HasForeignKey(dp => dp.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
