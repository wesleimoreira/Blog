using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;

namespace Blog.Application.Features.Comments.GetApprovedComments
{
    public sealed class GetApprovedCommentsHandler(ICommentRepository commentRepository, IPostRepository postRepository)
    {
        public async Task<IReadOnlyList<GetApprovedCommentResponse>> HandleAsync(Guid postId, CancellationToken cancellationToken = default)
        {
            var post = await postRepository.GetByIdAsync(postId, cancellationToken);

            if (post is null || !post.IsPublished)
                throw new NotFoundException("Post not found.");

            var comments = await commentRepository.GetApprovedByPostIdAsync(postId, cancellationToken);

            return
            [
                .. comments.Select(comment => new GetApprovedCommentResponse(
                comment.Id,
                comment.Content,
                $"{comment.User.FirstName} {comment.User.LastName}",
                comment.CreatedAt))
            ];
        }
    }
}
