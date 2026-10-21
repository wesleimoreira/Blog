using FluentValidation;

namespace Blog.Application.Features.Authentication.Refresh
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenValidator()
        {
            RuleFor(request => request.RefreshToken).NotEmpty();
        }
    }
}
