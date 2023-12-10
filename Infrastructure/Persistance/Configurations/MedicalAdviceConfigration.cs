using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configurations
{
    internal class MedicalAdviceConfigration : IEntityTypeConfiguration<MedicalAdvice>
    {
        public void Configure(EntityTypeBuilder<MedicalAdvice> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(100);
            builder.Property(a => a.Content).HasMaxLength(int.MaxValue);
            builder.Property(a => a.ImageUrl).HasMaxLength(450);
            builder.Property(a => a.PublishedON);
            builder.HasIndex(a => a.Title).IsUnique();
            builder.HasOne(a => a.Author)
                .WithMany()
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
