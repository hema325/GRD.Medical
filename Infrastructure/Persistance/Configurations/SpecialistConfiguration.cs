using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class SpecialistConfiguration : IEntityTypeConfiguration<Speciality>
    {
        public void Configure(EntityTypeBuilder<Speciality> builder)
        {
            builder.Property(s => s.Name).HasMaxLength(200);
            builder.Property(s => s.ImageUrl).HasMaxLength(450);

            builder.HasIndex(s => s.Name).IsUnique(true);
        }
    }
}
