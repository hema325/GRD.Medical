using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class UserChatBotConfiguration : IEntityTypeConfiguration<UserChatBotMessage>
    {
        public void Configure(EntityTypeBuilder<UserChatBotMessage> builder)
        {
            builder.Property(p => p.Content).HasMaxLength(500);

            builder.HasOne(p => p.Owner).WithMany().HasForeignKey(p => p.OwnerId);
        }
    }
}
