using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionBlogProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Infrastructure.EntityTypeConfig
{
    public class GenreConfig : BaseEntityConfig<Genre>
    {
        public override void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(100);
            //builder.Property(x => x.Status).IsRequired(true);

            // Many-to-One ilişkisi
            builder.HasMany(g => g.Posts)
               .WithOne(p => p.Genre)
               .HasForeignKey(p => p.GenreId);


            base.Configure(builder);
        }
    }
}
