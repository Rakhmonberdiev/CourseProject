
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seed
{
    public sealed class UserSeeder(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        public async Task SeedAsync()
        {
            await EnsureRoleAsync("admin");
            await EnsureRoleAsync("user");
            await EnsureUserAsync(email: "j.m.raxmonberdiyev@gmail.com", userName: "rakh",password: "1",role:"admin");
            await EnsureUserAsync(email: "rakh@gmail.com", userName: "Jamoliddin", password: "1",role:"user");

        }

        private async Task EnsureUserAsync(string email, string userName, string password,string role)
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
                if (!await userManager.IsInRoleAsync(user, role))
                {
                    var addRole = await userManager.AddToRoleAsync(user, role);
                    if (!addRole.Succeeded)
                        throw new InvalidOperationException(
                            $"Failed to add user '{email}' to role '{role}': " +
                            $"{string.Join(", ", addRole.Errors.Select(e => e.Description))}"
                        );
                }
            }
        }
        private async Task EnsureRoleAsync(string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole<Guid>(roleName);
              
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                    throw new InvalidOperationException(
                        $"Failed to create role '{roleName}': " +
                        $"{string.Join(", ", result.Errors.Select(e => e.Description))}"
                    );
            }
        }
    }

    
}
