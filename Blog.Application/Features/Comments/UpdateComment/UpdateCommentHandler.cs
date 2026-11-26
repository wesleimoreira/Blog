using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;
using FluentValidation;

namespace Blog.Application.Features.Comments.UpdateComment
{
    public sealed class UpdateCommentHandler(ICommentRepository commentRepository, IUnitOfWork unitOfWork, UpdateCommentValidator validator)
    {
        public async Task HandleAsync(Guid commentId, UpdateCommentRequest request, Guid currentUserId, bool isAdmin, CancellationToken cancellationToken = default)
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var comment = await commentRepository.GetByIdAsync(commentId, cancellationToken) ?? throw new NotFoundException("Comment not found.");

            if (!isAdmin && comment.UserId != currentUserId)
                throw new NotFoundException("Comment not found.");

            comment.UpdateContent(request.Content);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
