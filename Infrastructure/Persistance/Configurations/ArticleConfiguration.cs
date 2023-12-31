﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    internal class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(200);
            builder.Property(p => p.ImageUrl).HasMaxLength(450);

            builder.HasIndex(p => p.Title).IsUnique();
        }
    }
}
