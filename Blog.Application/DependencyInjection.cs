using Blog.Application.Features.Authentication.Login;
using Blog.Application.Features.Authentication.Refresh;
using Blog.Application.Features.Categories.CreateCategory;
using Blog.Application.Features.Categories.DeleteCategory;
using Blog.Application.Features.Categories.GetCategories;
using Blog.Application.Features.Categories.GetCategoryById;
using Blog.Application.Features.Categories.UpdateCategory;
using Blog.Application.Features.Tags.CreateTag;
using Blog.Application.Features.Tags.DeleteTag;
using Blog.Application.Features.Tags.GetTagById;
using Blog.Application.Features.Tags.GetTags;
using Blog.Application.Features.Tags.UpdateTag;
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
            services.AddScoped<CreateCategoryValidator>();

            // Register category handlers
            services.AddScoped<CreateCategoryHandler>();
            services.AddScoped<GetCategoriesHandler>();
            services.AddScoped<GetCategoryByIdHandler>();
            services.AddScoped<UpdateCategoryValidator>();
            services.AddScoped<UpdateCategoryHandler>();
            services.AddScoped<DeleteCategoryHandler>();

            // Register tags handlers
            services.AddScoped<CreateTagValidator>();
            services.AddScoped<CreateTagHandler>();
            services.AddScoped<GetTagsHandler>();
            services.AddScoped<GetTagByIdHandler>();
            services.AddScoped<UpdateTagValidator>();
            services.AddScoped<UpdateTagHandler>();
            services.AddScoped<DeleteTagHandler>();

            return services;
        }
    }
}
