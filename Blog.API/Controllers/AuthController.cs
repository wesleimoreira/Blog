using Blog.API.Common.Responses;
using Blog.Application.Features.Authentication.Login;
using Blog.Application.Features.Authentication.Refresh;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(LoginHandler loginHandler, RefreshTokenHandler refreshTokenHandler) : ControllerBase
    {
        private readonly LoginHandler _loginHandler = loginHandler;
        private readonly RefreshTokenHandler _refreshTokenHandler = refreshTokenHandler;


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

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _refreshTokenHandler.HandleAsync(request, cancellationToken);

            var response = ApiResponse<AuthenticationResponse>.Ok(result, "Token refreshed successfully.");

            return Ok(response);
        }

    }
}
