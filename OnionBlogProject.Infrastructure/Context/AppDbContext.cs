﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnionBlogProject.Domain.Entities;
using OnionBlogProject.Infrastructure.EntityTypeConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionBlogProject.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //public AppDbContext() // Migration oluştururken Hata almamak için boş bir constructor oluşturduk.
        //{
            
        //}
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }


        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=G™RKEMH; Database=OnionBlogProjectDb; Trusted_Connection=true");
            optionsBuilder.UseSqlServer("Server=G™RKEMH; Database=OnionBlogProjectDb; Uid=sa; Pwd=123;");

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppuserConfig());
            builder.ApplyConfiguration(new AuthorConfig());
            builder.ApplyConfiguration(new GenreConfig());
            builder.ApplyConfiguration(new PostConfig());

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "Admin",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = new Guid().ToString(),
            }, new IdentityRole
            {
                Id = "Member",
                Name = "Member",
                NormalizedName = "MEMBER",
                ConcurrencyStamp = new Guid().ToString(),
            });


            base.OnModelCreating(builder);
        }
    }
}
