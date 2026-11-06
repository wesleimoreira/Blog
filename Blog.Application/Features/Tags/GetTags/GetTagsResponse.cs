namespace Blog.Application.Features.Tags.GetTags
{
    public sealed record GetTagsResponse(Guid Id, string Name, string Slug);
}