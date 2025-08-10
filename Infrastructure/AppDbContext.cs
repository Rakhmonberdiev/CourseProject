using Domain.Inventories;
using Domain.Items;
using Domain.Tags;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure
{
    public sealed class AppDbContext : IdentityDbContext<AppUser,IdentityRole<Guid>,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}

        public DbSet<Inventory> Inventories => Set<Inventory>();
        public DbSet<InventoryAccess> InventoryAccesses => Set<InventoryAccess>();
        public DbSet<InventoryField> InventoryFields => Set<InventoryField>();
        public DbSet<CustomIdFormat> CustomIdFormats => Set<CustomIdFormat>();
        public DbSet<CustomIdElement> CustomIdElements => Set<CustomIdElement>();
        public DbSet<DiscussionPost> DiscussionPosts => Set<DiscussionPost>();
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
        public DbSet<ItemFieldValue> ItemFieldValues => Set<ItemFieldValue>();
        public DbSet<ItemLike> ItemLikes => Set<ItemLike>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<InventoryTag> InventoryTags => Set<InventoryTag>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
