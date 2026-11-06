using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;

namespace Blog.Application.Features.Categories.DeleteCategory
{
    public class DeleteCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Category was not found.");
           
            var hasPosts = await _categoryRepository.HasPostsAsync(id, cancellationToken);

            if (hasPosts)
                throw new ConflictException("Category cannot be deleted because it has associated posts.");

            _categoryRepository.Remove(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}