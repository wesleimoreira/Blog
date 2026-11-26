using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;

namespace Blog.Application.Features.Posts.DeletePost
{
    public class DeletePostHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task HandleAsync(Guid id, Guid currentUserId, bool isAdmin, CancellationToken cancellationToken = default)
        {
            var post = await _postRepository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Post was not found.");

            if (!isAdmin && post.AuthorId != currentUserId)
                throw new NotFoundException("Post was not found.");

            _postRepository.Remove(post);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}