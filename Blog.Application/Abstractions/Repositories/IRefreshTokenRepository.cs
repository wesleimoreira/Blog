using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken = default);

        Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

        Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

        Task RevokeAllActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
