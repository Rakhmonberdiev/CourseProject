using Application.Abstractions.Auth;
using Application.Abstractions.Discussion;
using Application.Abstractions.Inventory;
using Infrastructure.Extensions;
using Infrastructure.Seed;
using Infrastructure.Services.Auth;
using Infrastructure.Services.Discussion;
using Infrastructure.Services.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration conf)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conf.GetConnectionString("PostgresConnection")));

            services.AddIdentityServices();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IInventoryRepository, InventoryRepository>();

            services.AddScoped<IDiscussionRepository, DiscussionRepository>();
            return services;
        }
    }
}
