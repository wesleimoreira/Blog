namespace Blog.Application.Features.Users.RegisterUser
{
    public sealed record RegisterUserRequest(
      string FirstName,
      string LastName,
      string Email,
      string Password);
}
