namespace Blog.Application.Features.Posts.GetPostById
{
    public sealed record GetPostByIdResponse(
        Guid Id,
        string Title,
        string Slug,
        string Summary,
        string Content,
        bool IsPublished,
        DateTime? PublishedAt,
        Guid AuthorId,
        string AuthorName,
        Guid CategoryId,
        string CategoryName,
        IReadOnlyList<string> Tags);
}