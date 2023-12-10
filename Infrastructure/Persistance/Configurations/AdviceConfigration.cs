using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class AdviceConfigration : IEntityTypeConfiguration<Advice>
    {
        public void Configure(EntityTypeBuilder<Advice> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(200);
            builder.Property(a => a.ImageUrl).HasMaxLength(450);
            builder.Property(a => a.PublishedOn);

            builder.HasIndex(a => a.Title).IsUnique();
            builder.HasOne(a => a.Author).WithMany().HasForeignKey(a => a.AuthorId);

        }
    }
}
