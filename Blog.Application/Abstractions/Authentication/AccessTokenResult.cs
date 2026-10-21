namespace Blog.Application.Abstractions.Authentication
{
    public sealed record AccessTokenResult(string AccessToken, DateTime ExpiresAt);
}
