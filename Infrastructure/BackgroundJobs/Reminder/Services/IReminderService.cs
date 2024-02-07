namespace Infrastructure.BackgroundJobs.Reminder.Services
{
    internal interface IReminderService
    {
        Task RemindAsync();
    }
}
