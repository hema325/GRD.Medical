using Infrastructure.Persistance.Conversions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(c => c.Content).HasMaxLength(1500);

            builder.OwnsOne(c => c.Media, builder=>
            {
                builder.Property(m => m.Url).HasMaxLength(450);
            });

            builder.HasOne(c => c.Owner).WithMany().HasForeignKey(c => c.OwnerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(c => c.Replies).WithOne().HasForeignKey(c => c.ReplyTo).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(c => c.Post).WithMany().HasForeignKey(c => c.PostId);
        }
    }
}
