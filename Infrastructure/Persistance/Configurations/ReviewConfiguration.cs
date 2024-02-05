using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(r => r.Content)
                .HasMaxLength(500);

            builder.HasOne(r => r.Doctor).WithMany().HasForeignKey(r => r.DoctorId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(r => r.Owner).WithMany().HasForeignKey(r => r.OwnerId);

            builder.HasIndex(r => r.ReviewedOn);
        }
    }
}
