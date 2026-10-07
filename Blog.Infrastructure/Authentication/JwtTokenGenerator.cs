using Blog.Application.Abstractions.Authentication;
using Blog.Application.Features.Authentication.Login;
using Blog.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Infrastructure.Authentication
{
    public class JwtTokenGenerator(IOptions<JwtSettings> settings) : IJwtTokenGenerator
    {
        private readonly JwtSettings _settings = settings.Value;

        public AuthenticationResponse GenerateToken(User user)
        {
            var expiresAt = DateTime.UtcNow.AddMinutes(_settings.ExpirationInMinutes);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new(ClaimTypes.Email,user.Email),
                new(ClaimTypes.Role,user.Role.Name),
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthenticationResponse(
             accessToken,
             expiresAt,
             user.Id,
             user.FirstName,
             user.LastName,
             user.Email,
             user.Role.Name);
        }
    }
}
