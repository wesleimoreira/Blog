using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
