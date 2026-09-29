using Blog.Domain.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blog.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }

        public string? Description { get; private set; }

        public string Slug { get; private set; }

        private readonly List<Post> _posts = [];

        public IReadOnlyCollection<Post> Posts => _posts;

        protected Category()
        {
            Name = string.Empty;
            Slug = string.Empty;
        }

        public Category(string name, string? description, string slug)
        {
            Name = name;
            Description = description;
            Slug = slug;
        }
    }
}
