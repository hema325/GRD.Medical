using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistance.Conversions
{
    internal class DateTimeToUtcConverter : ValueConverter<DateTime, DateTime>
    {
        public DateTimeToUtcConverter() : base(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc))
        {
        }
    }
}
