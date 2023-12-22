using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Content).HasMaxLength(5000);
            builder.HasOne(p => p.Owner).WithMany().HasForeignKey(p => p.OwnerId);

            builder.OwnsMany(p => p.Medias, builder =>
            {
                builder.Property(p => p.Url).HasMaxLength(450);
            });
        }
    }
}
