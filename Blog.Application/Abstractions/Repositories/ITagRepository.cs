using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Repositories
{
    public interface ITagRepository
    {
        Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Tag?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default);

        Task AddAsync(Tag tag, CancellationToken cancellationToken = default);

        Task UpdateAsync(Tag tag, CancellationToken cancellationToken = default);

        Task DeleteAsync(Tag tag, CancellationToken cancellationToken = default);
    }
}
