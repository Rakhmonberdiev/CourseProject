namespace Application.Models.Auth
{
    public sealed record RegisterRequest(string Email, string Password, string userName);
}
