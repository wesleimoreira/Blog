namespace Blog.Domain.Common
{
    public static class DomainGuard
    {
        public static string Required(string value, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException($"{propertyName} is required.");
            }

            return value.Trim();
        }

        public static Guid Required(Guid value, string propertyName)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException($"{propertyName} is required.");
            }

            return value;
        }
    }
}
