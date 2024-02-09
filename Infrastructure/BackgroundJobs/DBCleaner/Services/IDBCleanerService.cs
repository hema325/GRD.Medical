
namespace Infrastructure.BackgroundJobs.DBCleaner.Services
{
    internal interface IDBCleanerService
    {
        Task CleanAsync();
    }
}
