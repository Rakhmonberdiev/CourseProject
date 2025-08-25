using Application.Abstractions.Auth;
using Application.Abstractions.Inventory;
using Infrastructure.Extensions;
using Infrastructure.Services.Auth;
using Infrastructure.Services.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            return services;
        }
    }
}
