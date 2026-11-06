using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;
using Blog.Domain.Entities;

namespace Blog.Application.Features.Tags.CreateTag
{
    public class CreateTagHandler(
        ITagRepository tagRepository,
        IUnitOfWork unitOfWork,
        CreateTagValidator validator)
    {
        private readonly ITagRepository _tagRepository = tagRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly CreateTagValidator _validator = validator;

        public async Task<Guid> HandleAsync(CreateTagRequest request, CancellationToken cancellationToken = default)
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

            var tag = new Tag(request.Name);

            var slugExists = await _tagRepository.ExistsBySlugAsync(tag.Slug, cancellationToken: cancellationToken);

            if (slugExists)
                throw new ConflictException("A tag with this name already exists.");

            await _tagRepository.AddAsync(tag, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return tag.Id;
        }
    }
}