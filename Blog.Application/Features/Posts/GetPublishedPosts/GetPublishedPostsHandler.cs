using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Models;

namespace Blog.Application.Features.Posts.GetPublishedPosts
{
    public class GetPublishedPostsHandler(IPostRepository postRepository)
    {
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<PagedResult<GetPublishedPostResponse>> HandleAsync(int page, int pageSize, Guid? categoryId = null, Guid? tagId = null, string? search = null, CancellationToken cancellationToken = default)
        {
            var result = await _postRepository.GetPublishedAsync(
                    page,
                    pageSize,
                    categoryId,
                    tagId,
                    search,
                    cancellationToken);

            var items = result.Items.Select(post => new GetPublishedPostResponse(
                    post.Id,
                    post.Title,
                    post.Slug,
                    post.Summary,
                    post.PublishedAt,
                    post.AuthorId,
                    $"{post.Author.FirstName} {post.Author.LastName}",
                    post.CategoryId,
                    post.Category.Name,
                    [.. post.PostTags.Select(postTag => postTag.Tag.Name)]
            )).ToList();

            return new PagedResult<GetPublishedPostResponse>(
                items,
                result.Page,
                result.PageSize,
                result.TotalCount);
        }
    }
}