using Application.Common;
using Application.Models.Auth;

namespace Application.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request);
        Task<Result<AuthResponse>> LoginAsync(LoginRequest request);
        Task<Result> LogoutAsync();
    }
}
