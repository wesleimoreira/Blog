using Blog.API.Common.Responses;
using Blog.Application.Features.Users.RegisterUser;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController(RegisterUserHandler registerUserHandler) : ControllerBase
    {
        private readonly RegisterUserHandler _registerUserHandler = registerUserHandler;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var userId = await _registerUserHandler.HandleAsync(request, cancellationToken);

            var response = ApiResponse<object>.Ok(new { id = userId }, "User registered successfully.");

            return Created($"/api/users/{userId}", response);
        }
    }
}
