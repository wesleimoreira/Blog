using Blog.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Persistence.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string Hash(string password)
        {
            return _hasher.HashPassword(new object(), password);
        }

        public bool Verify(string password, string passwordHash)
        {
            var result = _hasher.VerifyHashedPassword(new object(), passwordHash, password);

            return result != PasswordVerificationResult.Failed;
        }
    }
}
