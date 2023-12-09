using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    internal class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            await _context.Database.MigrateAsync();
        }
    }
}
