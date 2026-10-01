using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default);

        Task AddAsync(Category category, CancellationToken cancellationToken = default);

        Task UpdateAsync(Category category, CancellationToken cancellationToken = default);

        Task DeleteAsync(Category category, CancellationToken cancellationToken = default);
    }
}
