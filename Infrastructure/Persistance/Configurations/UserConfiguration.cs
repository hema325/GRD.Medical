using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(20);
            builder.Property(u => u.LastName).HasMaxLength(20);
            builder.Property(u => u.Email).HasMaxLength(250);
            builder.Property(u => u.ImageUrl).HasMaxLength(450);
            builder.Property(u => u.Role).HasConversion(r => r.ToString(), r => Enum.Parse<Roles>(r)).HasMaxLength(40);

            builder.HasIndex(u => u.Email).IsUnique(true);

            builder.HasMany(u => u.RefreshTokens).WithOne(rt=>rt.User).HasForeignKey(t => t.UserId);
        }
    }
}
