using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;
using Blog.Application.Features.Categories.GetCategories;

namespace Blog.Application.Features.Categories.GetCategoryById
{
    public class GetCategoryByIdHandler(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<GetCategoriesResponse> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);

            if (category is null)
                throw new NotFoundException("Category was not found.");

            return new GetCategoriesResponse(category.Id, category.Name, category.Slug, category.Description);
        }
    }
}