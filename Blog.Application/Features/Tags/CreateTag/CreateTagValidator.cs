using FluentValidation;

namespace Blog.Application.Features.Tags.CreateTag
{
    public class CreateTagValidator : AbstractValidator<CreateTagRequest>
    {
        public CreateTagValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage("Tag name is required.")
                .MaximumLength(100)
                .WithMessage("Tag name must not exceed 100 characters.");
        }
    }
}