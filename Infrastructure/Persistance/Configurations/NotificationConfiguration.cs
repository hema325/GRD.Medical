using Infrastructure.Persistance.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(n => n.Content).HasMaxLength(450);
            builder.Property(n => n.InitiatorId).IsRequired(false);
            builder.Property(n => n.NotifiedOn).HasConversion<DateTimeToUtcConverter>();

            builder.HasOne(n => n.Owner).WithMany().HasForeignKey(n => n.OwnerId);
            builder.HasOne(n => n.Initiator).WithMany().HasForeignKey(n => n.InitiatorId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
