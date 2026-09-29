using Blog.Domain.Common;

namespace Blog.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }

        public string Slug { get; private set; }

        public string? Description { get; private set; }

        private readonly List<Post> _posts = [];

        public IReadOnlyCollection<Post> Posts => _posts;

        protected Category()
        {
            Name = string.Empty;
            Slug = string.Empty;
        }

        public Category(string name, string? description = null)
        {
            Name = DomainGuard.Required(name, nameof(Name));

            Slug = SlugGenerator.Generate(Name);

            Description = NormalizeDescription(description);
        }

        public void Update(string name, string? description = null)
        {
            Name = DomainGuard.Required(name, nameof(Name));

            Slug = SlugGenerator.Generate(Name);

            Description = NormalizeDescription(description);

            SetUpdatedAt();
        }

        private static string? NormalizeDescription(string? description)
        {
            return string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        }
    }
}
