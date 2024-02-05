using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class AppointmentMessageConfiguration : IEntityTypeConfiguration<AppointmentMessage>
    {
        public void Configure(EntityTypeBuilder<AppointmentMessage> builder)
        {
            builder.Property(m => m.Content).HasMaxLength(500).IsRequired(false);

            builder.OwnsMany(m => m.Medias, b =>
            {
                b.Property(p => p.Url).HasMaxLength(450);
            });

            builder.HasOne(m => m.Appointment).WithMany().HasForeignKey(m => m.AppointmentId);
            builder.HasOne(m => m.Sender).WithMany().HasForeignKey(m => m.SenderId);

            builder.HasIndex(m => m.MessagedOn);
        }
    }
}
