using Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class ItemLikeConfiguration : IEntityTypeConfiguration<ItemLike>
    {
        public void Configure(EntityTypeBuilder<ItemLike> b)
        {
            b.HasKey(x => new { x.ItemId, x.UserId });

            b.Property(x => x.CreatedAt).IsRequired();

            b.HasOne(x => x.Item)
                .WithMany(i => i.Likes)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
