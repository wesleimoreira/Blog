namespace Blog.Application.Features.Categories.GetCategories
{
    public sealed record GetCategoriesResponse(Guid Id, string Name, string Slug, string? Description);
}