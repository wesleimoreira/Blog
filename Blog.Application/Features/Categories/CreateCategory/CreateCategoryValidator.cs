using FluentValidation;

namespace Blog.Application.Features.Categories.CreateCategory
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage("Category name is required.")
                .MaximumLength(100)
                .WithMessage("Category name must not exceed 100 characters.");

            RuleFor(request => request.Description)
                .MaximumLength(500)
                .WithMessage("Category description must not exceed 500 characters.")
                .When(request => !string.IsNullOrWhiteSpace(request.Description));
        }
    }
}