using Application.Abstractions.Auth;
using Application.Common;
using Application.Models.Auth;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Auth
{
    public sealed class AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager) : IAuthService
    {
        public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return Result<AuthResponse>.Fail("Invalid email or password");

            var ok = await userManager.CheckPasswordAsync(user, request.Password);
            if (!ok)
                return Result<AuthResponse>.Fail("Invalid email or password");

            await signInManager.SignInAsync(user, request.isPersistent);
  
            var roles = await userManager.GetRolesAsync(user);
            return Result<AuthResponse>.Ok(new AuthResponse(user.UserName!, roles.ToArray()));
        }

        public async Task<Result> LogoutAsync()
        {
            await signInManager.SignOutAsync();
            return Result.Ok("Signed out");
        }

        public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request)
        {
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                UserName = request.userName
            };
            var res = await userManager.CreateAsync(user, request.Password);
            if (!res.Succeeded)
                return Result<AuthResponse>.Fail(res.Errors.Select(e => e.Description).ToArray());

            await signInManager.SignInAsync(user, isPersistent: true);

            var roles = await userManager.GetRolesAsync(user);

            return Result<AuthResponse>.Ok(new AuthResponse(user.UserName!, roles.ToArray()));
        }
    }
}
