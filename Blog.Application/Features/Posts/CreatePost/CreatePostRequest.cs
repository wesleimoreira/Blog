namespace Blog.Application.Features.Posts.CreatePost
{
    public sealed record CreatePostRequest(string Title, string Summary, string Content, Guid CategoryId, IReadOnlyList<Guid> TagIds);
}