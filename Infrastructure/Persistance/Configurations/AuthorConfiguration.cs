using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(80);
            builder.Property(p => p.JobTitle).HasMaxLength(80);
            builder.Property(u => u.ImageUrl).HasMaxLength(450);

            builder.HasIndex(p => p.Name).IsUnique();
        }
    }
}
