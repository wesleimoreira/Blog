using Blog.Domain.Common;

namespace Blog.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; private set; }

        public string Slug { get; private set; }

        public string Summary { get; private set; }

        public string Content { get; private set; }

        public bool IsPublished { get; private set; }

        public DateTime? PublishedAt { get; private set; }

        public Guid AuthorId { get; private set; }

        public User Author { get; private set; } = null!;

        public Guid CategoryId { get; private set; }

        public Category Category { get; private set; } = null!;

        private readonly List<Comment> _comments = [];

        public IReadOnlyCollection<Comment> Comments => _comments;

        private readonly List<PostTag> _postTags = [];

        public IReadOnlyCollection<PostTag> PostTags => _postTags;

        protected Post()
        {
            Title = string.Empty;
            Slug = string.Empty;
            Summary = string.Empty;
            Content = string.Empty;
        }

        public Post(string title, string slug, string summary, string content)
        {
            Title = title;
            Slug = slug;
            Summary = summary;
            Content = content;
        }

        public void Publish()
        {
            IsPublished = true;
            PublishedAt = DateTime.UtcNow;
        }
    }
}
