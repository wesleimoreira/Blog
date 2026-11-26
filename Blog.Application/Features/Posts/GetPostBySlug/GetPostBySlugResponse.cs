namespace Blog.Application.Features.Posts.GetPostBySlug
{
    public sealed record GetPostBySlugResponse(
        Guid Id,
        string Title,
        string Slug,
        string Summary,
        string Content,
        DateTime? PublishedAt,
        Guid AuthorId,
        string AuthorName,
        Guid CategoryId,
        string CategoryName,
        IReadOnlyList<string> Tags);
}