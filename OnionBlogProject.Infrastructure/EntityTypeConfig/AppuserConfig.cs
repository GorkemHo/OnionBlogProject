using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionBlogProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Infrastructure.EntityTypeConfig
{
    public class AppuserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            //AppUser IdentityUser'dan kalıtım aldığı için burada IdentityUser özelliklerini de configure edebilirim.
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NormalizedUserName).IsRequired(false);
            builder.Property(x => x.UserName).IsRequired(true);

            builder.Property(x => x.ImagePath).IsRequired(false);

            // Silmedim.
            base.Configure(builder);
        }
    }
}
