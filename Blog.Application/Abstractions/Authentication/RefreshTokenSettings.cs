namespace Blog.Application.Abstractions.Authentication
{
    public class RefreshTokenSettings
    {
        public int ExpirationInDays { get; init; } = 7;
    }
}
