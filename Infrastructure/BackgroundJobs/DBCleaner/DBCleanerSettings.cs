namespace Infrastructure.BackgroundJobs.DBCleaner
{
    internal class DBCleanerSettings
    {
        public const string SectionName = "DBCleaner";

        public int DelayInDays { get; set; }
    }
}
