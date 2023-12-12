using Infrastructure.Authentication.PasswordHasher;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    internal class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public ApplicationDbContextInitialiser(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task InitialiseAsync()
        {
            await _context.Database.MigrateAsync();
            await SeedAsync();
        }

        private async Task SeedAsync()
        {
            if(!await _context.Users.AnyAsync())
            {
                var users = new[] {
                    new User
                    {
                        FirstName = "Ibrahim",
                        LastName = "Moawad",
                        Email = "admin@gmail.com",
                        IsEmailConfirmed = true,
                        HashedPassword = _passwordHasher.HashPassword("Pa$$w0rd"),
                        Role = Roles.Admin,
                        JoinedOn = DateTime.UtcNow
                    }
                };

                _context.Users.AddRange(users);
                await _context.SaveChangesAsync();
            }
        }
    }
}
