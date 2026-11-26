using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;

namespace Blog.Application.Features.Posts.GetPostById
{
    public class GetPostByIdHandler(IPostRepository postRepository)
    {
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<GetPostByIdResponse> HandleAsync(Guid id, Guid currentUserId, bool isAdmin, CancellationToken cancellationToken = default)
        {
            var post = await _postRepository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Post was not found.");

            // Authors can only access their own posts.
            // Admins can access posts from any author.
            if (!isAdmin && post.AuthorId != currentUserId)
                throw new NotFoundException("Post was not found.");

            return new GetPostByIdResponse(
                post.Id,
                post.Title,
                post.Slug,
                post.Summary,
                post.Content,
                post.IsPublished,
                post.PublishedAt,
                post.AuthorId,
                $"{post.Author.FirstName} {post.Author.LastName}",
                post.CategoryId,
                post.Category.Name,
                [.. post.PostTags.Select(postTag => postTag.Tag.Name)]);
        }
    }
}