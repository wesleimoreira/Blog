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
            Name = NormalizeName(name);
        }

        public void UpdateName(string name)
        {
            Name = NormalizeName(name);
            SetUpdatedAt();
        }

        private static string NormalizeName(string name)
        {
            name = DomainGuard.Required(name, nameof(Name));

            return char.ToUpperInvariant(name[0]) + name[1..].ToLowerInvariant();
        }
    }
}
