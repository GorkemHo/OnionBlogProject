using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionBlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Infrastructure.EntityTypeConfig
{
    public class PostConfig : BaseEntityConfig<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.Title).IsRequired(true);
            builder.Property(x => x.Content).IsRequired(true);

            // Post sınıfı ile Author arasında Many-to-One ilişkisi
            builder.HasOne(p => p.Author)
                   .WithMany(a => a.Posts)
                   .HasForeignKey(p => p.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // Post sınıfı ile Genre arasında Many-to-One ilişkisi
            builder.HasOne(p => p.Genre)
                   .WithMany(g => g.Posts)
                   .HasForeignKey(p => p.GenreId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            base.Configure(builder);
        }
    }
}
