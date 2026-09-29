using Blog.Domain.Common;

namespace Blog.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; private set; }

        public Guid RoleId { get; private set; }

        public Role Role { get; private set; } = null!;

        private readonly List<Post> _posts = [];

        public IReadOnlyCollection<Post> Posts => _posts;

        private readonly List<Comment> _comments = [];

        public IReadOnlyCollection<Comment> Comments => _comments;

        protected User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
        }

        public User(string firstName, string lastName, string email, string passwordHash)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
