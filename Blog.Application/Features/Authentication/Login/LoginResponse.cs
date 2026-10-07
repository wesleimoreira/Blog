namespace Blog.Application.Features.Authentication.Login
{
    public sealed record LoginResponse(Guid UserId, string FirstName, string LastName, string Email, string Role);
}
