using Blog.Application.Abstractions.Repositories;

namespace Blog.Application.Features.Tags.GetTags
{
    public class GetTagsHandler(ITagRepository tagRepository)
    {
        private readonly ITagRepository _tagRepository = tagRepository;

        public async Task<IReadOnlyList<GetTagsResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var tags = await _tagRepository.GetAllAsync(cancellationToken);

            return [.. tags
                .Select(tag => new GetTagsResponse(
                    tag.Id,
                    tag.Name,
                    tag.Slug))];
        }
    }
}