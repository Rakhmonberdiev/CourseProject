namespace Application.Models.Auth
{
    public sealed record LoginRequest(string Email, string Password, bool isPersistent);
}
