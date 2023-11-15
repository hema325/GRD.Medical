using Microsoft.AspNetCore.DataProtection;

namespace Infrastructure.Authentication.PasswordHasher
{
    internal class PasswordHasherService : IPasswordHasher
    {
        private readonly IDataProtector _dataProtector;
        public PasswordHasherService(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("Infrastructure.Authentication.Helpers.PasswordHasherHelper");
        }
        public string HashPassword(string password)
            => _dataProtector.Protect(password);

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
            => _dataProtector.Unprotect(hashedPassword) == providedPassword;
    }
}
