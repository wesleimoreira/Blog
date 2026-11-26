using FluentValidation;

namespace Blog.Application.Features.Posts.CreatePost
{
    public class CreatePostValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostValidator()
        {
            RuleFor(request => request.Title)
                .NotEmpty()
                .WithMessage("Post title is required.")
                .MaximumLength(200)
                .WithMessage("Post title must not exceed 200 characters.");

            RuleFor(request => request.Summary)
                .NotEmpty()
                .WithMessage("Post summary is required.")
                .MaximumLength(500)
                .WithMessage("Post summary must not exceed 500 characters.");

            RuleFor(request => request.Content)
                .NotEmpty()
                .WithMessage("Post content is required.");

            RuleFor(request => request.CategoryId)
                .NotEmpty()
                .WithMessage("Category is required.");

            RuleFor(request => request.TagIds)
                .NotNull()
                .WithMessage("Tag list is required.");

            RuleForEach(request => request.TagIds)
                .NotEmpty()
                .WithMessage("Tag ID must not be empty.")
                .When(request => request.TagIds is not null);
        }
    }
}