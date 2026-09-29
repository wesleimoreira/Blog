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
            IsApproved = false;
        }

        public Comment(string content, Guid postId, Guid userId)
        {
            Content = DomainGuard.Required(content, nameof(Content));

            PostId = DomainGuard.Required(postId, nameof(PostId));

            UserId = DomainGuard.Required(userId, nameof(UserId));

            IsApproved = false;
        }

        public void UpdateContent(string content)
        {
            Content = DomainGuard.Required(content, nameof(Content));

            SetUpdatedAt();
        }

        public void Approve()
        {
            if (IsApproved)
                return;

            IsApproved = true;

            SetUpdatedAt();
        }

        public void Reject()
        {
            if (!IsApproved)
                return;

            IsApproved = false;

            SetUpdatedAt();
        }
    }
}
