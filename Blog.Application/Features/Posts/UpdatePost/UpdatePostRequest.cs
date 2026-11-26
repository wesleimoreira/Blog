namespace Blog.Application.Features.Posts.UpdatePost
{
    public sealed record UpdatePostRequest(
        string Title,
        string Summary,
        string Content,
        Guid CategoryId,
        IReadOnlyList<Guid> TagIds);
}