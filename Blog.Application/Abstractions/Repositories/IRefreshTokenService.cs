using Blog.Application.Abstractions.Authentication;

namespace Blog.Application.Abstractions.Repositories
{
    public interface IRefreshTokenService
    {
        RefreshTokenResult GenerateToken();

        string HashToken(string token);
    }
}
