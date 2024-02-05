using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class BillingInfoConfiguration : IEntityTypeConfiguration<BillingInfo>
    {
        public void Configure(EntityTypeBuilder<BillingInfo> builder)
        {
            builder.Property(b => b.Amount).HasPrecision(9, 2);
            builder.HasOne(b => b.Appointment).WithOne(b=>b.BillingInfo).HasForeignKey<BillingInfo>(b => b.AppointmentId);

            builder.HasIndex(b => b.PaidIn);
        }
    }
}
