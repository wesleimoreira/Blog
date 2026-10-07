using Blog.Application.Features.Authentication.Login;
using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Authentication
{
    public interface IJwtTokenGenerator
    {
        AuthenticationResponse GenerateToken(User user);
    }
}
