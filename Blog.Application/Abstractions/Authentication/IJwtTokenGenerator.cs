using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateAccessToken(User user);
    }
}
