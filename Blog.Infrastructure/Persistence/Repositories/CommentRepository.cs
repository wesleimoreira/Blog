
using Blog.Application.Abstractions.Repositories;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.Repositories;

public class CommentRepository(BlogDbContext context) : ICommentRepository
{
    private readonly BlogDbContext _context = context;

    public async Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Comments
            .Include(comment => comment.User)
            .Include(comment => comment.Post)
            .FirstOrDefaultAsync(comment => comment.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Comment>> GetByPostIdAsync(Guid postId, CancellationToken cancellationToken = default)
    {
        return await _context.Comments
            .AsNoTracking()
            .Include(comment => comment.User)
            .Where(comment => comment.PostId == postId)
            .OrderByDescending(comment => comment.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Comment>> GetApprovedByPostIdAsync(Guid postId, CancellationToken cancellationToken = default)
    {
        return await _context.Comments
            .AsNoTracking()
            .Include(comment => comment.User)
            .Where(comment =>
                comment.PostId == postId &&
                comment.IsApproved)
            .OrderByDescending(comment => comment.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Comment comment, CancellationToken cancellationToken = default)
    {
        await _context.Comments.AddAsync(comment, cancellationToken);
    }

    public void Remove(Comment comment)
    {
        _context.Comments.Remove(comment);
    }
}
