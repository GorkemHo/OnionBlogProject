using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionBlogProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Infrastructure.EntityTypeConfig
{
    public class AuthorConfig : BaseEntityConfig<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(80);
            builder.Property(x => x.ImagePath).IsRequired(true);

            base.Configure(builder);
        }
    }
}
