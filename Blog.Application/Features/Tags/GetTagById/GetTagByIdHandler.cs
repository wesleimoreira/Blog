using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;
using Blog.Application.Features.Tags.GetTags;

namespace Blog.Application.Features.Tags.GetTagById
{
    public class GetTagByIdHandler(ITagRepository tagRepository)
    {
        private readonly ITagRepository _tagRepository = tagRepository;

        public async Task<GetTagsResponse> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var tag = await _tagRepository.GetByIdAsync(id, cancellationToken);

            return tag is null
                ? throw new NotFoundException("Tag was not found.")
                : new GetTagsResponse(tag.Id, tag.Name, tag.Slug);
        }
    }
}