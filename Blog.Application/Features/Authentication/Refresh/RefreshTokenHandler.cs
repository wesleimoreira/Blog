using Blog.Application.Abstractions.Authentication;
using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;
using Blog.Application.Features.Authentication.Login;
using Blog.Domain.Entities;

namespace Blog.Application.Features.Authentication.Refresh
{
    public class RefreshTokenHandler(IRefreshTokenRepository refreshTokenRepository, IRefreshTokenService refreshTokenService, IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork, RefreshTokenValidator validator)
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
        private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly RefreshTokenValidator _validator = validator;

        public async Task<AuthenticationResponse> HandleAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
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

            var tokenHash = _refreshTokenService.HashToken(request.RefreshToken);

            var storedRefreshToken = await _refreshTokenRepository.GetByTokenHashAsync(tokenHash, cancellationToken);

            if (storedRefreshToken is null || !storedRefreshToken.IsActive)
            {
                throw new UnauthorizedException("Invalid or expired refresh token.");
            }

            var user = storedRefreshToken.User;

            if (user is null || user.Role is null)
            {
                throw new UnauthorizedException("Invalid or expired refresh token.");
            }

            // Invalida o refresh token utilizado.
            storedRefreshToken.Revoke();

            // Gera um novo access token.
            var accessToken = _jwtTokenGenerator.GenerateToken(user);

            // Gera o próximo refresh token.
            var newRefreshTokenResult = _refreshTokenService.GenerateToken();

            var newRefreshTokenHash = _refreshTokenService.HashToken(newRefreshTokenResult.Token);

            var newRefreshToken = new RefreshToken(user.Id, newRefreshTokenHash, newRefreshTokenResult.ExpiresAt);

            await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);

            // Persiste a revogação e o novo token juntos.
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthenticationResponse(
                accessToken.AccessToken,
                accessToken.ExpiresAt,
                newRefreshTokenResult.Token,
                newRefreshTokenResult.ExpiresAt,
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Role.Name);
        }
    }
}
