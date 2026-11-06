using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Repositories
{
    public interface ITagRepository
    {
        Task<IReadOnlyList<Tag>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Tag?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> ExistsBySlugAsync(string slug, Guid? exceptId = null, CancellationToken cancellationToken = default);

        Task<bool> HasPostsAsync(Guid tagId, CancellationToken cancellationToken = default);

        Task AddAsync(Tag tag, CancellationToken cancellationToken = default);

        void Remove(Tag tag);
    }
}