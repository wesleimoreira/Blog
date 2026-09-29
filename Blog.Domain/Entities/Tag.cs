using Blog.Domain.Common;

namespace Blog.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; private set; }

        public string Slug { get; private set; }

        private readonly List<PostTag> _postTags = [];

        public IReadOnlyCollection<PostTag> PostTags => _postTags;

        protected Tag()
        {
            Name = string.Empty;
            Slug = string.Empty;
        }

        public Tag(string name)
        {
            Name = DomainGuard.Required(name, nameof(Name));

            Slug = SlugGenerator.Generate(Name);
        }

        public void UpdateName(string name)
        {
            Name = DomainGuard.Required(name, nameof(Name));

            Slug = SlugGenerator.Generate(Name);

            SetUpdatedAt();
        }
    }
}
