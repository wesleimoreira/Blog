using FluentValidation;

namespace Blog.Application.Features.Tags.UpdateTag
{
    public class UpdateTagValidator : AbstractValidator<UpdateTagRequest>
    {
        public UpdateTagValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage("Tag name is required.")
                .MaximumLength(100)
                .WithMessage("Tag name must not exceed 100 characters.");
        }
    }
}