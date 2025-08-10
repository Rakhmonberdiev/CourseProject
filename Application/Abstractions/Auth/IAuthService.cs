using Application.Common;
using Application.Models.Auth;

namespace Application.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request, CancellationToken ct = default);
        Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default);
        Task<Result> LogoutAsync(CancellationToken ct = default);
    }
}
