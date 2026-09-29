using Blog.Domain.Common;

namespace Blog.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; private set; }

        public bool IsApproved { get; private set; }

        public Guid PostId { get; private set; }

        public Post Post { get; private set; } = null!;

        public Guid UserId { get; private set; }

        public User User { get; private set; } = null!;

        protected Comment()
        {
            Content = string.Empty;
        }

        public Comment(string content)
        {
            Content = content;
        }

        public void Approve()
        {
            IsApproved = true;
        }
    }
}
