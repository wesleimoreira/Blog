using Blog.API.Common.Responses;
using Blog.Application.Features.Authentication.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(LoginHandler loginHandler) : ControllerBase
    {
        private readonly LoginHandler _loginHandler = loginHandler;

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _loginHandler.HandleAsync(request, cancellationToken);

            var response = ApiResponse<AuthenticationResponse>.Ok(result, "Login successful.");

            return Ok(response);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var email = User.FindFirstValue(ClaimTypes.Email);

            var role = User.FindFirstValue(ClaimTypes.Role);

            return Ok(ApiResponse<object>.Ok(
            new
            {
                userId,
                email,
                role
            }));
        }
    }
}
