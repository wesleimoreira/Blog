using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;
using Blog.Domain.Entities;
using FluentValidation;

namespace Blog.Application.Features.Comments.CreateComment
{

    public sealed class CreateCommentHandler(
        ICommentRepository commentRepository,
        IPostRepository postRepository,
        IUnitOfWork unitOfWork,
        CreateCommentValidator validator)
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly CreateCommentValidator _validator = validator;

        public async Task<Guid> HandleAsync(CreateCommentRequest request, Guid userId, CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken);

            if (post is null || !post.IsPublished)
                throw new NotFoundException("Post not found.");

            var comment = new Comment(
                request.Content,
                request.PostId,
                userId);

            await _commentRepository.AddAsync(comment, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return comment.Id;
        }
    }

}
