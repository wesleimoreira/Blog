using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Authentication
{
    public interface IJwtTokenGenerator
    {
        AccessTokenResult GenerateToken(User user);
    }
}
