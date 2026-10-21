using Blog.Application.Abstractions.Authentication;
using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;
using Blog.Domain.Entities;

namespace Blog.Application.Features.Authentication.Login
{
    public class LoginHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator, LoginValidator validator, IRefreshTokenRepository refreshTokenRepository, IRefreshTokenService refreshTokenService, IUnitOfWork unitOfWork)
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
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

            var accessToken = _jwtTokenGenerator.GenerateToken(user);

            var refreshTokenResult = _refreshTokenService.GenerateToken();

            var refreshTokenHash = _refreshTokenService.HashToken(refreshTokenResult.Token);

            var refreshToken = new RefreshToken(user.Id, refreshTokenHash, refreshTokenResult.ExpiresAt);

            await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthenticationResponse(
                accessToken.AccessToken,
                accessToken.ExpiresAt,
                refreshTokenResult.Token,
                refreshTokenResult.ExpiresAt,
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Role.Name);
        }
    }
}
