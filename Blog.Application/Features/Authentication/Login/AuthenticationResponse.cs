namespace Blog.Application.Features.Authentication.Login
{
    public sealed record AuthenticationResponse(
      string AccessToken,
      DateTime ExpiresAt,
      string RefreshToken,
      DateTime RefreshTokenExpiresAt,
      Guid UserId,
      string FirstName,
      string LastName,
      string Email,
      string Role);
}
