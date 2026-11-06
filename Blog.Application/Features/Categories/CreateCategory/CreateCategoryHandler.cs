using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;
using Blog.Domain.Entities;

namespace Blog.Application.Features.Categories.CreateCategory
{
    public class CreateCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, CreateCategoryValidator validator)
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly CreateCategoryValidator _validator = validator;

        public async Task<Guid> HandleAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default)
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

            var category = new Category(request.Name, request.Description);

            var slugExists = await _categoryRepository.ExistsBySlugAsync(category.Slug, cancellationToken: cancellationToken);

            if (slugExists)
                throw new ConflictException("A category with this name already exists.");

            await _categoryRepository.AddAsync(category, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}