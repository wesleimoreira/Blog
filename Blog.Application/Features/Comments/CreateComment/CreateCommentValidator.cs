using FluentValidation;

namespace Blog.Application.Features.Comments.CreateComment
{
    public sealed class CreateCommentValidator : AbstractValidator<CreateCommentRequest>
    {
        public CreateCommentValidator()
        {
            RuleFor(comment => comment.Content).NotEmpty().MaximumLength(1000);

            RuleFor(comment => comment.PostId).NotEmpty();
        }
    }
}
