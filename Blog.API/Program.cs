using Blog.API.Common.Exceptions;
using Blog.Application;
using Blog.Application.Abstractions.Authentication;
using Blog.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>() ?? throw new InvalidOperationException("JWT settings were not configured.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy("CanReadPosts", policy => policy.RequireRole("Reader", "Author", "Admin"))
    .AddPolicy("CanManagePosts", policy => policy.RequireRole("Author", "Admin"))
    .AddPolicy("CanManageUsers", policy => policy.RequireRole("Admin"))
    .AddPolicy("CanManageCategories", policy => policy.RequireRole("Admin"))
    .AddPolicy("CanManageTags", policy =>    policy.RequireRole("Admin"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();