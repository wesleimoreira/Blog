using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;

namespace Blog.Application.Features.Comments.ApproveComment
{
    public sealed class ApproveCommentHandler(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
    {
        public async Task HandleAsync(Guid commentId, CancellationToken cancellationToken = default)
        {
            var comment = await commentRepository.GetByIdAsync(commentId, cancellationToken) ?? throw new NotFoundException("Comment not found.");

            comment.Approve();

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
