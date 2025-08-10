using Domain.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Value)
                .HasMaxLength(64)
                .IsRequired();

            b.HasIndex(x => x.Value).IsUnique();
        }
    }
}
