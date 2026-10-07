namespace Blog.Application.Abstractions.Authentication
{
    public class JwtSettings
    {
        public string Issuer { get; init; } = string.Empty;

        public string Audience { get; init; } = string.Empty;

        public string SecretKey { get; init; } = string.Empty;

        public int ExpirationInMinutes { get; init; }
    }
}
