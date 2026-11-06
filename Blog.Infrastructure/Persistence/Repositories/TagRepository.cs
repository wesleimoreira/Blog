using Blog.Application.Abstractions.Repositories;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.Repositories
{
    public class TagRepository(BlogDbContext context) : ITagRepository
    {
        private readonly BlogDbContext _context = context;

        public async Task<IReadOnlyList<Tag>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Tags.AsNoTracking().OrderBy(tag => tag.Name).ToListAsync(cancellationToken);
        }

        public async Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Tags.FirstOrDefaultAsync(tag => tag.Id == id, cancellationToken);
        }

        public async Task<Tag?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _context.Tags.FirstOrDefaultAsync(tag => tag.Slug == slug, cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Tags.AnyAsync(tag => tag.Name == name, cancellationToken);
        }

        public async Task<bool> ExistsBySlugAsync(string slug, Guid? exceptId = null, CancellationToken cancellationToken = default)
        {
            return await _context.Tags.AnyAsync(tag => tag.Slug == slug && (!exceptId.HasValue || tag.Id != exceptId.Value), cancellationToken);
        }

        public async Task AddAsync(Tag tag, CancellationToken cancellationToken = default)
        {
            await _context.Tags.AddAsync(tag, cancellationToken);
        }

        public void Remove(Tag tag)
        {
            _context.Tags.Remove(tag);
        }

        public async Task<bool> HasPostsAsync(Guid tagId, CancellationToken cancellationToken = default)
        {
            return await _context.PostTags.AnyAsync(postTag => postTag.TagId == tagId, cancellationToken);
        }
    }
}