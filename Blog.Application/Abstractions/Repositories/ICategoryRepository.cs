using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Repositories
{
    public interface ICategoryRepository
    {
        Task<bool> HasPostsAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<bool> ExistsBySlugAsync(string slug, Guid? exceptId = null, CancellationToken cancellationToken = default);

        Task AddAsync(Category category, CancellationToken cancellationToken = default);

        void Remove(Category category);
    }
}
