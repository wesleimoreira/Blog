using Blog.Domain.Common;

namespace Blog.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; private set; }

        public bool IsApproved { get; private set; }

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
