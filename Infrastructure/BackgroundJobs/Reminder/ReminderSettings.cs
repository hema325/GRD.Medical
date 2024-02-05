namespace Infrastructure.BackgroundJobs.Reminder
{
    internal class ReminderSettings
    {
        public const string SectionName = "Reminder";

        public int DelayInSeconds { get; set; }
        public int CacheInMinutes { get; set; }
    }
}
