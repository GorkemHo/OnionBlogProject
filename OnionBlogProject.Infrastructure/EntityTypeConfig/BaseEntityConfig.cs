﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using OnionBlogProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Infrastructure.EntityTypeConfig
{
    public abstract class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : class, IBaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.DeleteDate).IsRequired(false);
            builder.Property(x => x.Status).IsRequired(true);
        }
    }
}
