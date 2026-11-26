using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Models;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.Repositories
{
    public class PostRepository(BlogDbContext context) : IPostRepository
    {
        private readonly BlogDbContext _context = context;

        public async Task<PagedResult<Post>> GetPublishedAsync(int page, int pageSize, Guid? categoryId = null, Guid? tagId = null, string? search = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Posts
                .AsNoTracking()
                .Where(post => post.IsPublished);

            if (categoryId.HasValue)
            {
                query = query.Where(post => post.CategoryId == categoryId.Value);
            }

            if (tagId.HasValue)
            {
                query = query.Where(post => post.PostTags.Any(postTag => postTag.TagId == tagId.Value));
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchTerm = search.Trim();

                query = query.Where(post => post.Title.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var posts = await query
                .Include(post => post.Author)
                .Include(post => post.Category)
                .Include(post => post.PostTags)
                    .ThenInclude(postTag => postTag.Tag)
                .OrderByDescending(post => post.PublishedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<Post>(
                posts,
                page,
                pageSize,
                totalCount);
        }

        public async Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Posts
                .Include(post => post.Author)
                .Include(post => post.Category)
                .Include(post => post.PostTags)
                    .ThenInclude(postTag => postTag.Tag)
                .FirstOrDefaultAsync(post => post.Id == id, cancellationToken);
        }

        public async Task<Post?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _context.Posts
                .Include(post => post.Author)
                .Include(post => post.Category)
                .Include(post => post.PostTags)
                    .ThenInclude(postTag => postTag.Tag)
                .FirstOrDefaultAsync(post => post.Slug == slug, cancellationToken);
        }

        public async Task<bool> ExistsBySlugAsync(string slug, Guid? exceptId = null, CancellationToken cancellationToken = default)
        {
            return await _context.Posts.AnyAsync(post => post.Slug == slug && (!exceptId.HasValue || post.Id != exceptId.Value), cancellationToken);
        }

        public async Task AddAsync(Post post, CancellationToken cancellationToken = default)
        {
            await _context.Posts.AddAsync(post, cancellationToken);
        }

        public void Remove(Post post)
        {
            _context.Posts.Remove(post);
        }
    }
}