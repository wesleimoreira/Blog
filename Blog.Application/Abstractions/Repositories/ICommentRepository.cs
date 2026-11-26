
using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Repositories;

public interface ICommentRepository
{
    Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Comment>> GetByPostIdAsync(Guid postId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Comment>> GetApprovedByPostIdAsync(Guid postId, CancellationToken cancellationToken = default);

    Task AddAsync(Comment comment, CancellationToken cancellationToken = default);

    void Remove(Comment comment);
}
