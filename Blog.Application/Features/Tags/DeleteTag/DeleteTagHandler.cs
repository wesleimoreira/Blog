using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;

namespace Blog.Application.Features.Tags.DeleteTag
{
    public class DeleteTagHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork)
    {
        private readonly ITagRepository _tagRepository = tagRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var tag = await _tagRepository.GetByIdAsync(id, cancellationToken);

            if (tag is null)
                throw new NotFoundException("Tag was not found.");

            var hasPosts = await _tagRepository.HasPostsAsync(id, cancellationToken);

            if (hasPosts)
                throw new ConflictException("Tag cannot be deleted because it is associated with posts.");

            _tagRepository.Remove(tag);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}