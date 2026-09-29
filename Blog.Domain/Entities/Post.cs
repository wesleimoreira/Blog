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

        private readonly List<PostTag> _postTags = [];
        public IReadOnlyCollection<PostTag> PostTags => _postTags;

        private readonly List<Comment> _comments = [];
        public IReadOnlyCollection<Comment> Comments => _comments;

        protected Post()
        {
            Title = string.Empty;
            Slug = string.Empty;
            Summary = string.Empty;
            Content = string.Empty;

            IsPublished = false;
            PublishedAt = null;
        }

        public Post(string title, string summary, string content, Guid authorId, Guid categoryId)
        {
            Title = DomainGuard.Required(title, nameof(Title));

            Slug = SlugGenerator.Generate(Title);

            Summary = DomainGuard.Required(summary, nameof(Summary));

            Content = DomainGuard.Required(content, nameof(Content));

            AuthorId = DomainGuard.Required(authorId, nameof(AuthorId));

            CategoryId = DomainGuard.Required(categoryId, nameof(CategoryId));

            IsPublished = false;
            PublishedAt = null;
        }

        public void Update(string title, string summary, string content, Guid categoryId)
        {
            Title = DomainGuard.Required(title, nameof(Title));

            Slug = SlugGenerator.Generate(Title);

            Summary = DomainGuard.Required(summary, nameof(Summary));

            Content = DomainGuard.Required(content, nameof(Content));

            CategoryId = DomainGuard.Required(categoryId, nameof(CategoryId));

            SetUpdatedAt();
        }

        public void Publish()
        {
            if (IsPublished)
                return;

            IsPublished = true;
            PublishedAt = DateTime.UtcNow;

            SetUpdatedAt();
        }

        public void Unpublish()
        {
            if (!IsPublished)
                return;

            IsPublished = false;
            PublishedAt = null;

            SetUpdatedAt();
        }
    }
}
