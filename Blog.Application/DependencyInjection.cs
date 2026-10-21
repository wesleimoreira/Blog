using Blog.Application.Features.Authentication.Login;
using Blog.Application.Features.Authentication.Refresh;
using Blog.Application.Features.Users.RegisterUser;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<RegisterUserValidator>();
            services.AddScoped<LoginValidator>();
            services.AddScoped<LoginHandler>();
            services.AddScoped<RefreshTokenValidator>();
            services.AddScoped<RefreshTokenHandler>();

            return services;
        }
    }
}
