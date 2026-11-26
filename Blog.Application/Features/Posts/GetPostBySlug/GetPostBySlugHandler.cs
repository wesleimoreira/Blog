using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;

namespace Blog.Application.Features.Posts.GetPostBySlug
{
    public class GetPostBySlugHandler(IPostRepository postRepository)
    {
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<GetPostBySlugResponse> HandleAsync(string slug, CancellationToken cancellationToken = default)
        {
            var post = await _postRepository.GetBySlugAsync(slug, cancellationToken);

            if (post is null || !post.IsPublished)
                throw new NotFoundException("Post was not found.");

            return new GetPostBySlugResponse(
                post.Id,
                post.Title,
                post.Slug,
                post.Summary,
                post.Content,
                post.PublishedAt,
                post.AuthorId,
                $"{post.Author.FirstName} {post.Author.LastName}",
                post.CategoryId,
                post.Category.Name,
                [.. post.PostTags.Select(postTag => postTag.Tag.Name)]);
        }
    }
}