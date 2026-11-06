namespace Blog.Application.Features.Categories.CreateCategory
{
    public sealed record CreateCategoryRequest(string Name, string? Description);
}