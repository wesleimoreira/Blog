using Blog.Application.Common.Models;
using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Repositories
{
    public interface IPostRepository
    {
        Task<PagedResult<Post>> GetPublishedAsync(int page, int pageSize, Guid? categoryId = null, Guid? tagId = null, string? search = null, CancellationToken cancellationToken = default);

        Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Post?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);

        Task<bool> ExistsBySlugAsync(string slug, Guid? exceptId = null, CancellationToken cancellationToken = default);

        Task AddAsync(Post post, CancellationToken cancellationToken = default);

        void Remove(Post post);
    }
}