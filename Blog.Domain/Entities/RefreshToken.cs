using Blog.Domain.Common;

namespace Blog.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public Guid UserId { get; private set; }

        public string TokenHash { get; private set; } = string.Empty;

        public DateTime ExpiresAt { get; private set; }

        public DateTime? RevokedAt { get; private set; }

        public User User { get; private set; } = null!;

        private RefreshToken() { }

        public RefreshToken(Guid userId, string tokenHash, DateTime expiresAt)
        {
            UserId = DomainGuard.Required(userId, nameof(UserId));

            TokenHash = DomainGuard.Required(tokenHash, nameof(TokenHash));

            if (expiresAt <= DateTime.UtcNow)
            {
                throw new DomainException("Refresh token expiration must be in the future.");
            }

            ExpiresAt = expiresAt;
        }

        public bool IsActive => RevokedAt is null && ExpiresAt > DateTime.UtcNow;

        public void Revoke()
        {
            if (RevokedAt is not null)
            {
                throw new DomainException("Refresh token has already been revoked.");
            }

            RevokedAt = DateTime.UtcNow;
            SetUpdatedAt();
        }
    }
}
