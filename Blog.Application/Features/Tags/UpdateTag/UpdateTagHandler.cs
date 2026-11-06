using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;
using Blog.Domain.Common;

namespace Blog.Application.Features.Tags.UpdateTag
{
    public class UpdateTagHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork, UpdateTagValidator validator)
    {
        private readonly ITagRepository _tagRepository = tagRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UpdateTagValidator _validator = validator;

        public async Task HandleAsync(Guid id, UpdateTagRequest request, CancellationToken cancellationToken = default)
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

            var tag = await _tagRepository.GetByIdAsync(id, cancellationToken);

            if (tag is null)
                throw new NotFoundException("Tag was not found.");

            var newSlug = SlugGenerator.Generate(request.Name);

            var slugExists = await _tagRepository.ExistsBySlugAsync(newSlug, id, cancellationToken);

            if (slugExists)
                throw new ConflictException("A tag with this name already exists.");

            tag.UpdateName(request.Name);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}