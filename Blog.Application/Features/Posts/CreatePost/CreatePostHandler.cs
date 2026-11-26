using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;
using Blog.Domain.Entities;

namespace Blog.Application.Features.Posts.CreatePost
{
    public class CreatePostHandler(IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository, IUnitOfWork unitOfWork, CreatePostValidator validator)
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ITagRepository _tagRepository = tagRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly CreatePostValidator _validator = validator;

        public async Task<Guid> HandleAsync(CreatePostRequest request, Guid authorId, CancellationToken cancellationToken = default)
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

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken) ?? throw new NotFoundException("Category was not found.");
            var tagIds = request.TagIds.Distinct().ToList();

            foreach (var tagId in tagIds)
            {
                var tag = await _tagRepository.GetByIdAsync(tagId, cancellationToken) ?? throw new NotFoundException($"Tag {tagId} was not found.");
            }

            var post = new Post(
                request.Title,
                request.Summary,
                request.Content,
                authorId,
                request.CategoryId);

            post.SetTags(tagIds);

            var slugExists = await _postRepository.ExistsBySlugAsync(post.Slug, cancellationToken: cancellationToken);

            if (slugExists)
                throw new ConflictException("A post with this title already exists.");

            await _postRepository.AddAsync(post, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return post.Id;
        }
    }
}