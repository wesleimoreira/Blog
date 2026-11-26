using Blog.Application.Features.Authentication.Login;
using Blog.Application.Features.Authentication.Refresh;
using Blog.Application.Features.Categories.CreateCategory;
using Blog.Application.Features.Categories.DeleteCategory;
using Blog.Application.Features.Categories.GetCategories;
using Blog.Application.Features.Categories.GetCategoryById;
using Blog.Application.Features.Categories.UpdateCategory;
using Blog.Application.Features.Posts.CreatePost;
using Blog.Application.Features.Posts.DeletePost;
using Blog.Application.Features.Posts.GetPostById;
using Blog.Application.Features.Posts.GetPostBySlug;
using Blog.Application.Features.Posts.GetPublishedPosts;
using Blog.Application.Features.Posts.PublishPost;
using Blog.Application.Features.Posts.UnpublishPost;
using Blog.Application.Features.Posts.UpdatePost;
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

            // Register Post handlers
            services.AddScoped<CreatePostValidator>();
            services.AddScoped<CreatePostHandler>();
            services.AddScoped<GetPublishedPostsHandler>();
            services.AddScoped<GetPostBySlugHandler>();
            services.AddScoped<GetPostByIdHandler>();
            services.AddScoped<UpdatePostValidator>();
            services.AddScoped<UpdatePostHandler>();
            services.AddScoped<DeletePostHandler>();
            services.AddScoped<PublishPostHandler>();
            services.AddScoped<UnpublishPostHandler>();

            return services;
        }
    }
}
