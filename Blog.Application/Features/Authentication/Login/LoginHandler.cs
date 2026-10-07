using Blog.Application.Abstractions.Authentication;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;

namespace Blog.Application.Features.Authentication.Login
{
    public class LoginHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator, LoginValidator validator)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly LoginValidator _validator = validator;

        public async Task<AuthenticationResponse> HandleAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group
                            .Select(error => error.ErrorMessage)
                            .ToArray());

                throw new ValidationException(errors);
            }

            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user is null)
                throw new UnauthorizedException("Invalid email or password.");

            var passwordIsValid = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!passwordIsValid)
                throw new UnauthorizedException("Invalid email or password.");

            return _jwtTokenGenerator.GenerateToken(user);
        }
    }
}
