using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;

namespace Blog.Application.Features.Comments.DeleteComment
{
    public sealed class DeleteCommentHandler(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
    {
        public async Task HandleAsync(Guid commentId, Guid currentUserId, bool isAdmin, CancellationToken cancellationToken = default)
        {
            var comment = await commentRepository.GetByIdAsync(commentId, cancellationToken) ?? throw new NotFoundException("Comment not found.");

            if (!isAdmin && comment.UserId != currentUserId)
                throw new NotFoundException("Comment not found.");

            commentRepository.Remove(comment);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
