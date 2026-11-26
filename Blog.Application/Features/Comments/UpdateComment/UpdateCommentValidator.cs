using FluentValidation;

namespace Blog.Application.Features.Comments.UpdateComment
{
    public sealed class UpdateCommentValidator : AbstractValidator<UpdateCommentRequest>
    {
        public UpdateCommentValidator()
        {
            RuleFor(comment => comment.Content).NotEmpty().MaximumLength(1000);
        }
    }
}
