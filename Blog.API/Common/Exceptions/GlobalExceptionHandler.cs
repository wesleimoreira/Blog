using Blog.API.Common.Responses;
using Blog.Application.Common.Exceptions;
using Blog.Domain.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace Blog.API.Common.Exceptions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An unhandled exception occurred.");

            var (statusCode, message) = exception switch
            {
                ConflictException => (StatusCodes.Status409Conflict, exception.Message),

                NotFoundException => (StatusCodes.Status404NotFound, exception.Message),

                UnauthorizedException => (StatusCodes.Status401Unauthorized, exception.Message),

                ValidationException => (StatusCodes.Status400BadRequest, exception.Message),

                DomainException => (StatusCodes.Status400BadRequest, exception.Message),

                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            };

            httpContext.Response.StatusCode = statusCode;

            var response = exception switch
            {
                ValidationException validationException => ApiResponse<object>.Fail(validationException.Message, validationException.Errors),
                _ => ApiResponse<object>.Fail(message)
            };

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
