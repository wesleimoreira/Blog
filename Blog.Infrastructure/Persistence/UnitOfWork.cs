using Blog.Application.Abstractions.Persistence;

namespace Blog.Infrastructure.Persistence
{
    public class UnitOfWork(BlogDbContext context) : IUnitOfWork
    {
        private readonly BlogDbContext _context = context;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
