using FluentValidation;

namespace Blog.Application.Features.Authentication.Login
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(request => request.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(request => request.Password)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
