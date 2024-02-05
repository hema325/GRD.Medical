using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(d => d.ConsultFee).HasPrecision(9,2);

            builder.HasOne(d => d.User).WithOne(u => u.Doctor).HasForeignKey<Doctor>(d => d.UserId);
            builder.HasOne(d => d.Speciality).WithMany().HasForeignKey(d=>d.SpecialityId);
            builder.HasMany(d => d.Languages).WithMany();

            builder.HasIndex(d => new { d.SpecialityId, d.Experience, d.ConsultFee });
        }
    }
}
