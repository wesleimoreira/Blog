using Blog.Domain.Common;

namespace Blog.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; private set; }

        private readonly List<User> _users = [];

        public IReadOnlyCollection<User> Users => _users;

        protected Role()
        {
            Name = string.Empty;
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}
