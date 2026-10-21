using Blog.Application.Abstractions.Authentication;
using Blog.Application.Abstractions.Repositories;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Blog.Infrastructure.Persistence.Authentication
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly RefreshTokenSettings _settings;

        public RefreshTokenService(IOptions<RefreshTokenSettings> settings)
        {
            _settings = settings.Value;

            if (_settings.ExpirationInDays <= 0)
            {
                throw new InvalidOperationException("Refresh token expiration must be greater than zero.");
            }
        }

        public RefreshTokenResult GenerateToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            var token = Convert.ToBase64String(randomBytes);

            var expiresAt = DateTime.UtcNow.AddDays(_settings.ExpirationInDays);

            return new RefreshTokenResult(token, expiresAt);
        }

        public string HashToken(string token)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(token);

            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var hashBytes = SHA256.HashData(tokenBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
