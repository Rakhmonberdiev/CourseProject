
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seed
{
    public sealed class UserSeeder(UserManager<AppUser> userManager)
    {
        public async Task SeedAsync()
        {
            await EnsureUserAsync(email: "j.m.raxmonberdiyev@gmail.com", userName: "rakh",password: "Admin123*");
            await EnsureUserAsync(email: "rakh@gmail.com", userName: "Jamoliddin", password: "Admin123*");
        }

        private async Task EnsureUserAsync(string email, string userName, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user is null)
            {
                user = new AppUser
                {
                    Id = Guid.NewGuid(),
                    UserName = userName,
                    Email = email,
                    EmailConfirmed = true
                };
                var create = await userManager.CreateAsync(user, password);
                if (!create.Succeeded)
                    throw new InvalidOperationException($"Failed to create user '{email}': {string.Join(", ", create.Errors.Select(e => e.Description))}");
            }
        }
    }

    
}
