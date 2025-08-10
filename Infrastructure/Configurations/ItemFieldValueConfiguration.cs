using Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class ItemFieldValueConfiguration : IEntityTypeConfiguration<ItemFieldValue>
    {
        public void Configure(EntityTypeBuilder<ItemFieldValue> b)
        {
            b.HasKey(x => x.Id);

            b.HasIndex(x => new { x.ItemId, x.FieldId }).IsUnique();

            b.HasOne(x => x.Item)
                .WithMany(i => i.FieldValues)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Field)
                .WithMany()
                .HasForeignKey(x => x.FieldId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
