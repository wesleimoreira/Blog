using Blog.Domain.Common;

namespace Blog.Domain.Entities
{
    public class PostTag
    {
        public Guid PostId { get; private set; }

        public Guid TagId { get; private set; }

        public Post Post { get; private set; } = null!;

        public Tag Tag { get; private set; } = null!;

        protected PostTag()
        {
        }

        public PostTag(Guid postId, Guid tagId)
        {
            PostId = postId;
            TagId = tagId;
        }
    }
