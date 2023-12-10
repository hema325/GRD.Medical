using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.PasswordHasher
{
    internal class PasswordHasherService : IPasswordHasher
    {
        private readonly IPasswordHasher<object> _passwordHasher;

        public PasswordHasherService(IPasswordHasher<object> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(string password)
            => _passwordHasher.HashPassword(new object(), password);

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
            => _passwordHasher.VerifyHashedPassword(new object(), hashedPassword, providedPassword) == PasswordVerificationResult.Success;
    }
}
