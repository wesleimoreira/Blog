namespace Blog.Application.Features.Categories.UpdateCategory
{
    public sealed record UpdateCategoryRequest(string Name, string? Description);
}