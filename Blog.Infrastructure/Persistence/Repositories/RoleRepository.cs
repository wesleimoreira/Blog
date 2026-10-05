using Blog.Application.Abstractions.Repositories;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.Repositories
{
    public class RoleRepository(BlogDbContext context) : IRoleRepository
    {
        private readonly BlogDbContext _context = context;

        public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Roles.FirstOrDefaultAsync(role => role.Id == id, cancellationToken);
        }

        public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Roles.FirstOrDefaultAsync(role => role.Name == name, cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Roles.AnyAsync(role => role.Id == id, cancellationToken);
        }
    }
}
