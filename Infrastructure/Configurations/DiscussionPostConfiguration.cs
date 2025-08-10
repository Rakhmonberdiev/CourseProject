using Domain.Inventories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class DiscussionPostConfiguration : IEntityTypeConfiguration<DiscussionPost>
    {
        public void Configure(EntityTypeBuilder<DiscussionPost> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Markdown)
                .IsRequired();

            b.HasIndex(x => new { x.InventoryId, x.CreatedAt });

            b.HasOne(x => x.Inventory)
                .WithMany(i => i.DiscussionPosts)
                .HasForeignKey(x => x.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Author)
                .WithMany()
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
