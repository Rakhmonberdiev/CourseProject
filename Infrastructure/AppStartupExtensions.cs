
using Infrastructure.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public static class AppStartupExtensions
    {
        public static async Task UseMigrationsAndSeedAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var sp = scope.ServiceProvider;
            var logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger("Startup");
            try
            {
                var db = sp.GetRequiredService<AppDbContext>();
                await db.Database.MigrateAsync();

                var userSeeder = ActivatorUtilities.CreateInstance<UserSeeder>(sp);
                await userSeeder.SeedAsync();
                var invSeeder = ActivatorUtilities.CreateInstance<InventorySeeder>(sp);
                await invSeeder.SeedAsync(db);
                logger.LogInformation("Migrations & seeding completed.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Migrations/Seeding failed.");
                throw;
            }
        }
    }
}
