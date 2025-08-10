namespace Application.Models.Auth
{
    public sealed record AuthResponse(string userName, string[] roles);
}
