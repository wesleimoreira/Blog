using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence
{
    public class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
    {
    }
}
