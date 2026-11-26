namespace Blog.Application.Features.Posts.GetPublishedPosts
{
    public sealed record GetPublishedPostResponse(
        Guid Id,
        string Title,
        string Slug,
        string Summary,
        DateTime? PublishedAt,
        Guid AuthorId,
        string AuthorName,
        Guid CategoryId,
        string CategoryName,
        IReadOnlyList<string> Tags);
}