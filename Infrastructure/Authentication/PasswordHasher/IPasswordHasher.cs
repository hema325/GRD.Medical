namespace Infrastructure.Authentication.PasswordHasher
{
    internal interface IPasswordHasher
    {
        public string HashPassword(string password);
        public bool VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}
