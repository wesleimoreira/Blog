using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;

namespace Blog.Application.Features.Posts.UpdatePost
{
    public class UpdatePostHandler(IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository, IUnitOfWork unitOfWork, UpdatePostValidator validator)
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ITagRepository _tagRepository = tagRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UpdatePostValidator _validator = validator;

        public async Task HandleAsync(Guid id, UpdatePostRequest request, Guid currentUserId, bool isAdmin, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(error => error.ErrorMessage).ToArray());

                throw new ValidationException(errors);
            }

            var post = await _postRepository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Post was not found.");

            if (!isAdmin && post.AuthorId != currentUserId)
                throw new NotFoundException("Post was not found.");

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken) ?? throw new NotFoundException("Category was not found.");
            var tagIds = request.TagIds.Distinct().ToList();

            foreach (var tagId in tagIds)
            {
                var tag = await _tagRepository.GetByIdAsync(tagId, cancellationToken) ?? throw new NotFoundException($"Tag {tagId} was not found.");
            }

            var updatedPost = new Blog.Domain.Entities.Post(
                request.Title,
                request.Summary,
                request.Content,
                post.AuthorId,
                request.CategoryId);

            var slugExists = await _postRepository.ExistsBySlugAsync(updatedPost.Slug, id, cancellationToken);

            if (slugExists)
                throw new ConflictException("A post with this title already exists.");

            post.Update(
                request.Title,
                request.Summary,
                request.Content,
                request.CategoryId);

            post.SetTags(tagIds);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}