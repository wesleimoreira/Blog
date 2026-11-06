using Blog.Application.Abstractions.Repositories;

namespace Blog.Application.Features.Categories.GetCategories
{
    public class GetCategoriesHandler(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<IReadOnlyList<GetCategoriesResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);

            return [.. categories
                .Select(category => new GetCategoriesResponse(
                    category.Id,
                    category.Name,
                    category.Slug,
                    category.Description))];
        }
    }
}