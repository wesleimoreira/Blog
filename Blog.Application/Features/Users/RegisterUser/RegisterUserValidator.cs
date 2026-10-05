using FluentValidation;

namespace Blog.Application.Features.Users.RegisterUser
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(request => request.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(request => request.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(request => request.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(request => request.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);
        }
    }
}
