using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Repositories
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Post?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);

        Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default);

        Task AddAsync(Post post, CancellationToken cancellationToken = default);

        Task UpdateAsync(Post post, CancellationToken cancellationToken = default);

        Task DeleteAsync(Post post, CancellationToken cancellationToken = default);
    }
}
