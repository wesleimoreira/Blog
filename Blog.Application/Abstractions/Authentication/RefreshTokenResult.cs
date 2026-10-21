namespace Blog.Application.Abstractions.Authentication
{
    public sealed record RefreshTokenResult(string Token, DateTime ExpiresAt);
}
