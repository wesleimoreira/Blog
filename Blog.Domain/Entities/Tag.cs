using Blog.Domain.Common;

namespace Blog.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; private set; }

        public string Slug { get; private set; }

        protected Tag()
        {
            Name = string.Empty;
            Slug = string.Empty;
        }

        public Tag(string name, string slug)
        {
            Name = name;
            Slug = slug;
        }
    }
}
