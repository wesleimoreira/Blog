using Blog.Application.Abstractions.Repositories;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository(BlogDbContext context) : ICategoryRepository
    {
        private readonly BlogDbContext _context = context;

        public async Task<bool> HasPostsAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Posts.AnyAsync(post => post.CategoryId == categoryId, cancellationToken);
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Categories.AsNoTracking().OrderBy(category => category.Name).ToListAsync(cancellationToken);
        }

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.FirstOrDefaultAsync(category => category.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsBySlugAsync(string slug, Guid? exceptId = null, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.AnyAsync(category => category.Slug == slug && (!exceptId.HasValue || category.Id != exceptId.Value), cancellationToken);
        }

        public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
        {
            await _context.Categories.AddAsync(category, cancellationToken);
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}