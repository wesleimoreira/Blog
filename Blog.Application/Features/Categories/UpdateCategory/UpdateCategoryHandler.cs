using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;

namespace Blog.Application.Features.Categories.UpdateCategory
{
    public class UpdateCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, UpdateCategoryValidator validator)
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UpdateCategoryValidator _validator = validator;

        public async Task HandleAsync(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken = default)
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

            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Category was not found.");

            var newSlug = Blog.Domain.Common.SlugGenerator.Generate(request.Name);

            var slugExists = await _categoryRepository.ExistsBySlugAsync(newSlug, id, cancellationToken);

            if (slugExists)
                throw new ConflictException("A category with this name already exists.");

            category.Update(request.Name, request.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}